using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PCMS.Application.Common.Dtos;
using PCMS.Application.Common.Interfaces;
using PCMS.Application.Common.Validators;
using PCMS.Application.Common.Extensions;
using PCMS.Domain.Entities;
using PCMS.Infrastructure.Caching;
using PCMS.Infrastructure.Search;
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("api/products")]
//[EnableCors("AllowAll")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ProductSearchEngine<Product> _searchEngine;
    private readonly SearchCache _cache;

    public ProductsController(
        IProductRepository productRepository,
        ProductSearchEngine<Product> searchEngine,
        SearchCache cache)
    {
        _productRepository = productRepository;
        _searchEngine = searchEngine;
        _cache = cache;
    }

    // GET /api/products?search=&categoryId=&page=&pageSize=
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        // 1. Input Validation
        page = page < 1 ? 1 : page;
        pageSize = Math.Clamp(pageSize, 1, 100);

        // 2. Consistent Cache Key
        string sanitizedSearch = search?.Trim().ToLower() ?? "none";
        string cacheKey = $"products:{sanitizedSearch}:{categoryId?.ToString() ?? "all"}:{page}:{pageSize}";

        if (_cache.TryGet(cacheKey, out var cached))
            return Ok(cached);

        // 3. Null Propagation & Safety
        var products = _productRepository.GetAll();
        if (products == null) return Ok(Enumerable.Empty<Product>());

        // 4. Filtering
        products = products
            .FilterByCategory(categoryId)
            .SearchByName(search);

        // 5. Logic Safety
        if (!string.IsNullOrWhiteSpace(search) && _searchEngine != null)
        {
            products = _searchEngine.Search(
                products,
                search,
                new Func<Product, string>[]
                {
                p => p.Name ?? string.Empty,
                p => p.Description ?? string.Empty
                });
        }

        var result = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // 6. Set cache with an expiration (Absolute/Sliding) to avoid stale data
        _cache.Set(cacheKey, result);

        return Ok(result);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var product = _productRepository.GetById(id);

        return product is null
            ? NotFound()
            : Ok(product);
    }

    // POST /api/products
    [HttpPost]
    public IActionResult Create([FromBody] CreateProductDto dto)
    {
        if (!ProductValidator.IsValid(dto))
            return BadRequest("Invalid product");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            SKU = dto.SKU,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _productRepository.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // PUT /api/products/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] CreateProductDto dto)
    {
        var existing = _productRepository.GetById(id);
        if (existing is null) return NotFound();

        if (!ProductValidator.IsValid(dto))
            return BadRequest("Invalid product");

        existing.Name = dto.Name;
        existing.SKU = dto.SKU;
        existing.Price = dto.Price;
        existing.Quantity = dto.Quantity;
        existing.CategoryId = dto.CategoryId;
        existing.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(existing);

        return NoContent();
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var existing = _productRepository.GetById(id);
        if (existing is null) return NotFound();

        _productRepository.Delete(id);

        return NoContent();
    }

    // BONUS: Custom JSON serialization endpoint
    [HttpGet("{id}/custom")]
    public IActionResult GetCustom(Guid id)
    {
        var product = _productRepository.GetById(id);
        if (product is null) return NotFound();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return new ContentResult
        {
            Content = JsonSerializer.Serialize(product, options),
            ContentType = "application/json"
        };
    }

    // BONUS: Manual model binding NB LOOK INTO
    [HttpPost("manual")]
    public async Task<IActionResult> CreateManual()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        var dto = JsonSerializer.Deserialize<CreateProductDto>(body);

        if (dto is null || !ProductValidator.IsValid(dto))
            return BadRequest();

        return Ok(dto);
    }
}
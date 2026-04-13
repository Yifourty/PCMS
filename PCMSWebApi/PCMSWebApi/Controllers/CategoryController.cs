using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PCMS.Application.Common.Dtos;
using PCMS.Application.Common.Interfaces;
using PCMS.Application.Common.Validators;
using PCMS.Application.Common.Extensions;
using PCMS.Domain.Entities;
using PCMS.Infrastructure.Caching;
using PCMS.Infrastructure.Search;
using PCMS.Application.Common.Services;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryTreeBuilder _categoryTreeBuilder;

    public CategoriesController(
        ICategoryRepository categoryRepository,
        CategoryTreeBuilder categoryTreeBuilder
        )
    {
        _categoryRepository = categoryRepository;
        _categoryTreeBuilder = categoryTreeBuilder;
    }

    // GET /api/categories
    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryRepository.GetAll();
        return !categories.Any()
            ? NotFound()
            : Ok(categories);
    }

    // GET /api/categories/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var category = _categoryRepository.GetById(id);

        return category is null
            ? NotFound()
            : Ok(category);
    }

    // POST /api/categories
    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryDto dto)
    {
        //if (!CategoryValidator.IsValid(dto))
        //    return BadRequest("Invalid category");

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Children = new List<Category>(),
            ParentCategoryId = dto.ParentCategoryId
        };

        _categoryRepository.Add(category);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    // GET /api/categories/tree
    [HttpGet("tree")]
    public IActionResult CategoryTree()
    {
        var categories = _categoryRepository.GetAll();
        return !categories.Any()
           ? NotFound()
           : Ok(_categoryTreeBuilder.BuildTree(categories));
    }

}
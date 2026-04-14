using Microsoft.AspNetCore.Mvc;
using PCMS.Application.Common.Dtos;
using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryRepository.GetAll();
        return !categories.Any()
            ? NotFound()
            : Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var category = _categoryRepository.GetById(id);

        return category is null
            ? NotFound()
            : Ok(category);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryDto dto)
    {

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Children = new List<Category>(),
            ParentCategoryId = dto.ParentCategoryId ?? null
        };

        _categoryRepository.Add(category);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpGet("tree")]
    public IActionResult CategoryTree()
    {
        var categories = _categoryRepository.GetAll();
        return !categories.Any()
           ? NotFound()
           : Ok(_categoryTreeBuilder.BuildTree(categories));
    }

}
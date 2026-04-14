using PCMS.Domain.Entities;

namespace PCMS.Application.Common.Dtos
{
    public record CategoryDto(
        Guid Id,
        string Name,
        string? Description,
        string SKU,
        Guid? ParentCategoryId,
        List<Category>? Children

    );

    public record CreateCategoryDto(
        string Name,
        string? Description,
        Guid? ParentCategoryId
    );
}

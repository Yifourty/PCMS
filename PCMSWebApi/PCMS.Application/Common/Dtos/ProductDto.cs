namespace PCMS.Application.Common.Dtos
{
    public record ProductDto(
        Guid Id,
        string Name,
        string? Description,
        string SKU,
        decimal Price,
        int Quantity,
        Guid CategoryId
    );

    public record CreateProductDto(
        string Name,
        string SKU,
        decimal Price,
        int Quantity,
        Guid CategoryId
    );
}

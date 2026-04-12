namespace PCMS.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<Category> Children { get; set; } = new();
    }
}

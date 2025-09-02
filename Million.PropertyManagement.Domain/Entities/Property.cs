namespace Million.PropertyManagement.Domain.Entities;
public class Property
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int YearBuilt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int? OwnerId { get; set; }
    public List<PropertyImage> Images { get; set; } = new();
    public List<PropertyTrace> Traces { get; set; } = new();
}

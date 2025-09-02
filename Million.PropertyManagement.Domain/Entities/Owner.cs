namespace Million.PropertyManagement.Domain.Entities;
public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public string? Photo { get; set; }
    public List<Property> Properties { get; set; } = new();
}

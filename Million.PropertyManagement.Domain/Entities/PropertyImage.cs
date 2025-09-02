namespace Million.PropertyManagement.Domain.Entities;
public class PropertyImage
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}

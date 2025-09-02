namespace Million.PropertyManagement.Domain.Entities;
public class PropertyTrace
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
}

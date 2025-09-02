namespace Million.PropertyManagement.Application.DTOs
{
    public record OwnerCreateDto(string Name, string Address, DateTime? Birthday, string? Photo);
    public record OwnerReadDto(int Id, string Name, string Address, DateTime? Birthday, string? Photo);
}

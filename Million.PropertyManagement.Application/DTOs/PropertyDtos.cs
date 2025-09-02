namespace Million.PropertyManagement.Application.DTOs;
public record PropertyCreateDto(string Address, string City, decimal Price, int YearBuilt, int? OwnerId);
public record PropertyUpdateDto(string Address, string City, decimal Price, int YearBuilt);
public record PropertyPriceUpdateDto(decimal NewPrice);
public record PropertyImageCreateDto(string Url, bool IsPrimary);
public record PropertyListFilter(string? City, decimal? MinPrice, decimal? MaxPrice, int Page = 1, int PageSize = 20);
public record PropertyReadDto(int Id, string Address, string City, decimal Price, int YearBuilt, IEnumerable<PropertyImageReadDto> Images);
public record PropertyImageReadDto(int Id, string Url, bool IsPrimary);
public record PropertyTraceDto(int PropertyId, DateTime DateSale, string Name, decimal Value, decimal Tax);


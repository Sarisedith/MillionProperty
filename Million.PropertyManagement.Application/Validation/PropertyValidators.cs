using FluentValidation;
using Million.PropertyManagement.Application.DTOs;
namespace Million.PropertyManagement.Application.Validation;
public class PropertyCreateValidator : AbstractValidator<PropertyCreateDto>
{
    public PropertyCreateValidator()
    {
        RuleFor(x => x.Address).NotEmpty().MaximumLength(200);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.YearBuilt).InclusiveBetween(1800, DateTime.UtcNow.Year);
    }
}
public class PropertyPriceUpdateValidator : AbstractValidator<PropertyPriceUpdateDto>
{
    public PropertyPriceUpdateValidator() => RuleFor(x => x.NewPrice).GreaterThan(0);
}

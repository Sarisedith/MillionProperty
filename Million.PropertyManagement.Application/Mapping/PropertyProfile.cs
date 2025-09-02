using AutoMapper;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Application.DTOs;
namespace Million.PropertyManagement.Application.Mapping;
public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<PropertyImage, PropertyImageReadDto>();
        CreateMap<Property, PropertyReadDto>().ForCtorParam("Images", opt => opt.MapFrom(src => src.Images));
        CreateMap<PropertyCreateDto, Property>();
        CreateMap<PropertyImageCreateDto, Property>();
        CreateMap<PropertyImageCreateDto, PropertyImage>();
        CreateMap<PropertyUpdateDto, Property>();
    }
}

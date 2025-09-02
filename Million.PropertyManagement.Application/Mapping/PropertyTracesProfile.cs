using AutoMapper;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;

namespace Million.PropertyManagement.Application.Mapping
{
    public class PropertyTracesProfile : Profile
    {
        public PropertyTracesProfile()
        {
            CreateMap<PropertyTraceDto, PropertyTrace>();
        }
    }
}

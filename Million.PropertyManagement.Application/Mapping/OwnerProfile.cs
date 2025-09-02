using AutoMapper;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
namespace Million.PropertyManagement.Application.Mapping
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<OwnerCreateDto, Owner>();
        }
    }
}

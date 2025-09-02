using AutoMapper;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Million.PropertyManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.PropertyManagement.Application.Services
{
    public class PropertyTracesService : IPropertyTracesService
    {
        private readonly IMapper _mapper;
        private readonly IPropertyTracesRepository _propertyTracesRepository;
        public PropertyTracesService(IMapper mapper, IPropertyTracesRepository propertyTracesRepository) { _mapper = mapper; _propertyTracesRepository = propertyTracesRepository; }
        public async Task<PropertyTrace> CreateAsync(PropertyTraceDto trace)
        {
            var entity = _mapper.Map<PropertyTrace>(trace);
            await _propertyTracesRepository.AddAsync(entity);
            await _propertyTracesRepository.SaveAsync();
            return _mapper.Map<PropertyTrace>(entity);
        }

        public async Task<List<PropertyTrace>?> GetByProperty(int propertyId)
        {
            return await _propertyTracesRepository.GetByPropertyAsync(propertyId);
        }
    }
}

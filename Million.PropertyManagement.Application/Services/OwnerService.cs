using AutoMapper;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;

namespace Million.PropertyManagement.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper) { _ownerRepository = ownerRepository; _mapper = mapper; }

        public async Task<Owner> AddAsync(OwnerCreateDto owner)
        {
            var entity = _mapper.Map<Owner>(owner);
            await _ownerRepository.AddAsync(entity);
            await _ownerRepository.SaveAsync();
            return entity;
        }

        public IQueryable<Owner> GetAll(string? name)
        {
            var query = _ownerRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(name)) query = query.Where(o => o.Name.Contains(name));
            return query;
        }

        public async Task<Owner?> GetById(int id)
        {
            return await _ownerRepository.GetByIdAsync(id);
        }
    }
}

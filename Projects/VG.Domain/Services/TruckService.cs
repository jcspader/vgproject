using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VG.Domain.Dto;
using VG.Domain.Shared;
using VG.Infra.Data.Entities;
using VG.Infra.Data.Repositories;


namespace VG.Domain.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _repository;
        private readonly IMapper _mapper;

        public TruckService(
            ITruckRepository repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TruckDto>> GetAllAsync()
        {
            var entity = await _repository.GetAllQueryable()
                                    .Include(i => i.Model)
                                    .ToArrayAsync();
            return _mapper.Map<IEnumerable<TruckDto>>(entity);
        }

        public async Task<TruckDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TruckDto>(entity);
        }

        public async Task AddAsync(TruckDto obj)
        {
            var objEntity = _mapper.Map<TruckEntity>(obj);
            await _repository.AddAsync(objEntity);
        }

        public async Task<Result<ProcessResult, bool>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return await _repository.DeleteAsync(entity);
        }

        public async Task<Result<ProcessResult, bool>> UpdateAsync(TruckDto obj)
        {
            var objEntity = _mapper.Map<TruckEntity>(obj);
            return await _repository.UpdateAsync(objEntity);
        }
    }
}

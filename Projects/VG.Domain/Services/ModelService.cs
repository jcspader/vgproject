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
    public class ModelService : IModelService
    {
        private readonly IModelRepository _repository;
        private readonly ITruckRepository _truckRepository;
        private readonly IMapper _mapper;

        public ModelService(
            IModelRepository repository,
            ITruckRepository truckRepository,
            IMapper mapper
            )
        {
            _repository = repository;
            _truckRepository = truckRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ModelDto>> GetAllAsync()
        {
            var entity = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ModelDto>>(entity);
        }

        public async Task<ModelDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ModelDto>(entity);
        }

        public async Task AddAsync(ModelDto obj)
        {
            var objEntity = _mapper.Map<ModelEntity>(obj);
            await _repository.AddAsync(objEntity);

            _mapper.Map<ModelDto>(objEntity);
        }

        public async Task<Result<ProcessResult, bool>> UpdateAsync(ModelDto obj)
        {
            var objEntity = _mapper.Map<ModelEntity>(obj);

            if (await HasChild(objEntity.Id))
                return new ProcessResult("You can't update a model referenced any truck.");

            return await _repository.UpdateAsync(objEntity);
        }

        public async Task<Result<ProcessResult, bool>> DeleteAsync(int id)
        {
            if (await HasChild(id))
                return new ProcessResult("You can't delete a model referenced any truck.");

            var objEntity = await _repository.GetByIdAsync(id);

            return await _repository.DeleteAsync(objEntity);
        }

        private async Task<bool> HasChild(int id)
        {
            return await _truckRepository.GetAllQueryable()
                                .AnyAsync(t => t.ModelId == id);
        }
    }
}

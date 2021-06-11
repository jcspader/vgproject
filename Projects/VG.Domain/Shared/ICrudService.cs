using System.Collections.Generic;
using System.Threading.Tasks;

namespace VG.Domain.Shared
{
    public interface ICrudService<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity obj);
        Task<Result<ProcessResult, bool>> UpdateAsync(TEntity obj);
        Task<Result<ProcessResult, bool>> DeleteAsync(int obj);
    }
}

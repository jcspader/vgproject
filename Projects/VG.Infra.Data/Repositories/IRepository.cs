using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VG.Infra.Data.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        Task<bool> DeleteAsync(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAllQueryable();
    }
}

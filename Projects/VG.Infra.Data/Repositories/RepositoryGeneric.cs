using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VG.Infra.Data.Context;

namespace VG.Infra.Data.Repositories
{
    public abstract class RepositoryGeneric<TEntity> : IRepository<TEntity>
            where TEntity : class
    {
        private readonly DataBaseContext dataBaseContext;

        public RepositoryGeneric(DataBaseContext dataBaseContext)
            => this.dataBaseContext = dataBaseContext;

        public async Task AddAsync(TEntity obj)
        {
            try
            {
                await dataBaseContext.Set<TEntity>()
                                    .AddAsync(obj);
                await dataBaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity obj)
        {
            try
            {
                dataBaseContext.Set<TEntity>()
                                .Remove(obj);
                var rowAffected = await dataBaseContext.SaveChangesAsync();

                return rowAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllQueryable()
                            .ToArrayAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await dataBaseContext.Set<TEntity>()
                                      .FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            try
            {
                return dataBaseContext.Set<TEntity>()
                        .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity obj)
        {
            try
            {
                dataBaseContext.Entry<TEntity>(obj).State = EntityState.Modified;
                var rowAffected = await dataBaseContext.SaveChangesAsync();

                return rowAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

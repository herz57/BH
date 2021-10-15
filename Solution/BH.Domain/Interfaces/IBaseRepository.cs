using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BH.Domain.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> FindAsync(object id);

        Task<ICollection<TEntity>> GetAllAsync();

        Task<ICollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task SaveChangesAsync();
    }
}

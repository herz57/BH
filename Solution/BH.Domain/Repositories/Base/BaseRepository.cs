using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected BhDbContext Context { get; set; }

        protected DbSet<TEntity> Entity { get; set; }

        public BaseRepository(BhDbContext context)
        {
            Context = context;
            Entity = Context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entity.Where(expression).ToListAsync();
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}

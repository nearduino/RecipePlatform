using Auth.Infrastructure.EfStructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.Base
{
    public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Add(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Update(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Remove(entity);

            if (persist)
                _context.SaveChanges();
        }

    }
}

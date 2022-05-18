using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.Base
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : class
    {
        DbSet<TEntity> GetAll();
        TEntity Add(TEntity entity, bool persist = true);
        TEntity Update(TEntity entity, bool persist = true);
        void Delete(TEntity entity, bool persist = true);
    }
}

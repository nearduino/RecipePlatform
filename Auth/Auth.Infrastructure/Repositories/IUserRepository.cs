using Auth.Infrastructure.DBO;
using Auth.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<int, UserDbo>
    {
        public UserDbo GetById(Guid id);
    }
}

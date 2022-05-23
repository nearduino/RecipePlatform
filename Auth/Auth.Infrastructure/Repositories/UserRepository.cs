using Auth.Infrastructure.DBO;
using Auth.Infrastructure.EfStructures;
using Auth.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<int, UserDbo>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public UserDbo GetById(Guid id)
        {
            var users = GetAll();           
            return users.First(r => r.Id == id);
        }
    }
}

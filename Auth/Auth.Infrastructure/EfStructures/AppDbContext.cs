using Auth.Infrastructure.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.EfStructures
{
    public class AppDbContext : DbContext
    {

        public DbSet<UserDbo> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {        

        }       

    }
   
}

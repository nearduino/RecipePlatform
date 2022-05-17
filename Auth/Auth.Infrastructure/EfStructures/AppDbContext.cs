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
              
            UserDbo user = new UserDbo
            {
                FirstName = "Djordje",
                LastName = "Djukic",
                UserName = "djuka6",
                Email = "djukicdjordje98@gmail.com",
                IsAdmin = true,
                Password = "12345"              
            };            
            Users.Add(user);
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbo>();
            base.OnModelCreating(modelBuilder);
        }

    }
   
}

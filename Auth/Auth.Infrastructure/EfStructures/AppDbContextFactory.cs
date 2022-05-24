using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.Infrastructure.EfStructures
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:internship-sql." +
                                        "database.windows.net,1433;Initial Catalog=Authorization;" +
                                        "Persist Security Info=False;User ID=InternshipAdmin;" +
                                        "Password=Levi9Internship;MultipleActiveResultSets=False;" +
                                        "Encrypt=True;" +
                                        "TrustServerCertificate=False;" +
                                        "Connection Timeout=30;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace RecipeHub.Infrastructure.EfStructures
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:internship-sql.database.windows.net,1433;Initial Catalog=Recipes;Persist Security Info=False;User ID=InternshipAdmin;Password=Levi9Internship;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new AppDbContext(optionsBuilder.Options);
        }

       
    }
}

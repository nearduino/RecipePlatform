using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<RecipeDbo> Recipes { get; set; }
        public DbSet<CommentDbo> Comments { get; set; }
        public DbSet<IngredientDbo> Ingredients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Recipes.Add(new RecipeDbo
            {
                Name = "S visnjom burek",
                Category = Category.Breakfast,
                CommentsDbo = new List<CommentDbo> { new CommentDbo { Rating = 10, Text = "Sjajno" } },
                Description = "Ovo je burek punjen visnjama",
                Id = 1,
                ImgSrc = "",
                Instructions = "",
                PreparationTime = 30,
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>()
            });
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeDbo>().OwnsMany(t => t.RecipeIngredientsDbo);
            base.OnModelCreating(modelBuilder);
        }
    }
}

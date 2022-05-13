using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Model;
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
            IngredientDbo ingr = new IngredientDbo
            {
                CaloriesPerUnit = 10,
                MeasureUnit = MeasureUnit.g,
                Name = "Lala"
            };
            Ingredients.Add(ingr);
            SaveChanges();
            RecipeDbo rec = new RecipeDbo
            {
                Name = "S visnjom burek",
                Category = Category.Breakfast,
                CommentsDbo = new List<CommentDbo> { new CommentDbo { Rating = 10, Text = "Sjajno" } },
                Description = "Ovo je burek punjen visnjama",
                ImgSrc = "",
                Instructions = "",
                PreparationTime = 30,
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>()
            };
            rec.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{Ingredient = ingr, Quantity = 2});
            Recipes.Add(rec);
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeDbo>().OwnsMany(t => t.RecipeIngredientsDbo);
            base.OnModelCreating(modelBuilder);
        }
    }
}

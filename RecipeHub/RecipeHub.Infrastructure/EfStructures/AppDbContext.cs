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
        public DbSet<ArticleDbo> Articles { get; set; }
        public DbSet<RecipeDbo> Recipes { get; set; }
        public DbSet<CommentDbo> Comments { get; set; }
        public DbSet<RecipeIngredientDbo> RecipeIngredientDbos { get; set; }
        public DbSet<IngredientDbo> Ingredients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //IngredientDbo ingr = new IngredientDbo
            //{
            //    CaloriesPerUnit = 10,
            //    MeasureUnit = MeasureUnit.g,
            //    Name = "Lala"
            //};
            //IngredientDbo ingr2 = new IngredientDbo
            //{
            //    CaloriesPerUnit = 20,
            //    MeasureUnit = MeasureUnit.ml,
            //    Name = "Bla"
            //};
            //Ingredients.Add(ingr);
            //Ingredients.Add(ingr2);
            //SaveChanges();
            //RecipeDbo rec = new RecipeDbo
            //{
            //    Name = "Sa visnjom burek",
            //    Category = Category.Breakfast,
            //    CommentsDbo = new List<CommentDbo> { new CommentDbo { Rating = 10, Text = "Sjajno" } },
            //    Description = "Ovo je burek punjen visnjama",
            //    ImgSrc = "",
            //    Instructions = "Stavis visnje u burek",
            //    PreparationTime = 30,
            //    UserId = Guid.Parse("5f5ea2d2-8eb3-4b01-8115-b3d44d9eb342"),
            //    RecipeIngredientsDbo = new List<RecipeIngredientDbo>()
            //};
            //rec.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDbo = ingr, Quantity = 2});
            //Recipes.Add(rec);
            //SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeDbo>().OwnsMany(t => t.RecipeIngredientsDbo);
            base.OnModelCreating(modelBuilder);
        }
    }
}

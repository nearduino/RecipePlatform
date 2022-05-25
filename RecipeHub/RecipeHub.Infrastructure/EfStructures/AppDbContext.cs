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

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeDbo>().OwnsMany(t => t.RecipeIngredientsDbo);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using RecipeHub.API.Middleware;
using RecipeHub.API.Swagger;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Services;
using RecipeHub.Domain.Services.Implementation;
using RecipeHub.Infrastructure.EfStructures;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Services;
using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Implementations;
using RecipeHub.Infrastructure.Repositories.DboRepos;

namespace RecipeHub.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecipeHubApi", Version = "v1" });
                c.OperationFilter<SwaggerHeaderParameter>();
            });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:internship-sql.database.windows.net,1433;Initial Catalog=Recipes;Persist Security Info=False;User ID=InternshipAdmin;Password=Levi9Internship;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IRecipeInfrastructureService, RecipeInfrastructureService>();
            services.AddScoped<IIngredientInfrastructureService, IngredientInfrastructureService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICommentInfrastructureService, CommentInfrastructureService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleInfrastructureService, ArticleInfrastructureService>();
            services.AddScoped<ICommentService, CommentService>();

            var builder = new ContainerBuilder();
            builder.RegisterAutoMapper(propertiesAutowired: false, AppDomain.CurrentDomain.GetAssemblies());
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeHub v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseJwtMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auth.Service;
using Auth.Model;
using Microsoft.OpenApi.Models;
using Auth.Infrastructure.EfStructures;
using Auth.Infrastructure.Repositories;
using Auth.Model.InfrastructureInterfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Auth.API
{
    public class Startup
    {        // add services to the DI container

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationApi", Version = "v1" });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:internship-sql.database.windows.net,1433;Initial Catalog=Authorization;Persist Security Info=False;User ID=InternshipAdmin;Password=Levi9Internship;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IUserRepository, UserRepository>();                     
            services.AddScoped<IUserInfrastructureService, UserInfrastructureService>();            
            services.AddScoped<IUserService, UserService>();
        }

        // configure the HTTP request pipeline
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using RecipeHub.Infrastructure.AutoMapperProfiles;

namespace RecipeHub.UnitTests.Base
{
    public class BaseFixture : IDisposable
    {
        public IMapper Mapper { get; set; }

        public BaseFixture()
        {
            var builder = new ContainerBuilder();
            List<Assembly> autoMapperAssemblies = new List<Assembly>()
            {
                typeof(AutoMapperComment).Assembly
            };
            builder.RegisterAutoMapper(false, autoMapperAssemblies.ToArray());
            var container = builder.Build();
            Mapper = container.Resolve<IMapper>();
        }

        public void Dispose()
        {
        }
    }
}

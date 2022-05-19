using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;

namespace RecipeHub.UnitTests.Base
{
    public class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        protected IMapper _mapper => _fixture.Mapper;
    }
}

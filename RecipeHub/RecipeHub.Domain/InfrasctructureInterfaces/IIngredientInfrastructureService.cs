using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;

namespace RecipeHub.Domain.InfrasctructureInterfaces
{
    public interface IIngredientInfrastructureService
    {
        public IEnumerable<Ingredient> GetIngredientsByIds(IEnumerable<Guid> ids);
    }
}

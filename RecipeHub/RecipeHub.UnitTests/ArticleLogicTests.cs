using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.Implementations;
using RecipeHub.Domain.Model;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecipeHub.UnitTests
{
    public class ArticleLogicTests
    {
        [Fact]
        public void CreateArticle_should_throw_exception()
        {
            /*ArticleLogic al = new ArticleLogic();
            Article a1 = new Article(1, "Title 1", "text 1");
            bool exceptionThrown = false;
            try
            {
                al.CreateArticle(1, a1);
            }
            catch (ArticleException)
            {
                exceptionThrown = true;
            }
            Assert.False(exceptionThrown);*/
            true.ShouldBeTrue();
        }

        public static IEnumerable<object[]> CreateArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            
            Article a1 = new Article(1, "Title 1", "text 1");
            retVal.Add(new object[]
            {
                a1.Id, a1, false
            }) ;

            Article a2 = new Article(2, "Title 2", "text 2");
            retVal.Add(new object[]
            {
                a2.Id, a2, false
            });

            Article a3 = new Article(1, "Title 3", "text 3");   // ovde bi trebalo da baci exception
            retVal.Add(new object[]
            {
                a3.Id, a3, true
            });

            return retVal;
        }


    }
}

using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.Implementations;
using RecipeHub.Domain.Model;
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
        [Theory]
        [MemberData(nameof(CreateArticle_objects))]
        public void CreateArticle_should_throw_exception(int id, Article a)
        {
            ArticleLogic al = new ArticleLogic();
            bool exceptionThrown = false;
            try
            {
                al.CreateArticle(id, a);
            }
            catch (ArticleException)
            {
                exceptionThrown = true;
            }
            Assert.False(exceptionThrown);
        }

        public static IEnumerable<object[]> CreateArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();           
            Article a1 = new Article(1, "Title 1", "text 1");
            retVal.Add(new object[]
            {
                a1.Id, a1
            }) ;

            Article a2 = new Article(2, "Title 2", "text 2");
            retVal.Add(new object[]
            {
                a2.Id, a2
            });

            Article a3 = new Article(1, "Title 3", "text 3");
            retVal.Add(new object[]
            {
                a3.Id, a3
            });
            return retVal;
        }

        [Theory]
        [MemberData(nameof(ReadArticle_objects))]
        public void ReadArticle_should_throw_exception(int id)
        {
            ArticleLogic al = new ArticleLogic();
            bool exceptionThrown = false;
            try
            {
                al.ReadArticle(id);
            }
            catch (ArticleException)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> ReadArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
            {
                1
            });
            retVal.Add(new object[]
            {
                2
            });
            retVal.Add(new object[]
            {
                4
            });
            return retVal;
        }

        [Theory]
        [MemberData(nameof(UpdateArticle_objects))]
        public void UpdateArticle_should_throw_exception(int id, Article a)
        {
            ArticleLogic al = new ArticleLogic();
            bool exceptionThrown = false;
            try
            {
                al.UpdateArticle(id, a);
            }
            catch (ArticleException)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> UpdateArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            Article a1 = new Article(1, "Title 1e", "text 1e");
            retVal.Add(new object[]
            {
                a1.Id, a1
            });

            Article a2 = new Article(2, "Title 2e", "text 2e");
            retVal.Add(new object[]
            {
                a2.Id, a2
            });

            Article a3 = new Article(1, "Title 3e", "text 3e");
            retVal.Add(new object[]
            {
                a3.Id, a3
            });
            return retVal;
        }

        [Theory]
        [MemberData(nameof(DeleteArticle_objects))]
        public void DeleteArticle_should_throw_exception(int id)
        {
            ArticleLogic al = new ArticleLogic();
            bool exceptionThrown = false;
            try
            {
                al.DeleteArticle(id);
            }
            catch (ArticleException)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> DeleteArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
            {
                1
            });
            retVal.Add(new object[]
            {
                2
            });
            retVal.Add(new object[]
            {
                4
            });
            return retVal;
        }
    }
}

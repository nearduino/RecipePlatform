using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.Implementations;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecipeHub.xUnitTests
{
    public class ArticleTests
    {
        [Theory]
        [MemberData(nameof(CreateArticle_objects))]
        public void CreateArticle_should_throw_exception(Article a)
        {
            ArticleService al = new ArticleService();
            bool exceptionThrown = false;
            try
            {
                al.CreateArticle(a);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> CreateArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            Guid aId1 = Guid.Parse("128455ee-4b6b-4b62-9b12-e3f17fabb1cb");
            Guid uId1 = Guid.Parse("f3a7d20d-ed1b-4b97-a4be-4a62865e885b");
            Article a1 = new Article(aId1, uId1, "Title 1", "text 1");
            retVal.Add(new object[]
            {
                a1
            }) ;

            return retVal;
        }

        [Theory]
        [MemberData(nameof(ReadArticle_objects))]
        public void ReadArticle_should_throw_exception(Guid id)
        {
            ArticleService al = new ArticleService();
            bool exceptionThrown = false;
            try
            {
                al.ReadArticle(id);
            }
            catch (Exception)
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
                Guid.NewGuid(),
            });
            retVal.Add(new object[]
            {
                Guid.NewGuid()
            });
            retVal.Add(new object[]
            {
                Guid.NewGuid()
            });
            return retVal;
        }

        [Theory]
        [MemberData(nameof(UpdateArticle_objects))]
        public void UpdateArticle_should_throw_exception(Article a)
        {
            ArticleService al = new ArticleService();
            bool exceptionThrown = false;
            try
            {
                al.UpdateArticle(a);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> UpdateArticle_objects()
        {
            List<object[]> retVal = new List<object[]>();
            Guid id1 = Guid.NewGuid();
            Article a1 = new Article(id1, Guid.NewGuid(), "Title 1e", "text 1e");
            retVal.Add(new object[]
            {
                a1
            });
            Guid id2 = Guid.NewGuid();
            Article a2 = new Article(id2, Guid.NewGuid(), "Title 2e", "text 2e");
            retVal.Add(new object[]
            {
                a2
            });
            Guid id3 = id1;
            Article a3 = new Article(id3, Guid.NewGuid(), "Title 3e", "text 3e");
            retVal.Add(new object[]
            {
                a3
            });
            return retVal;
        }

        [Theory]
        [MemberData(nameof(DeleteArticle_objects))]
        public void DeleteArticle_should_throw_exception(Guid id)
        {
            ArticleService al = new ArticleService();
            bool exceptionThrown = false;
            try
            {
                al.DeleteArticle(id);
            }
            catch (Exception)
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
                Guid.NewGuid()
            });
            retVal.Add(new object[]
            {
                Guid.NewGuid()
            });
            retVal.Add(new object[]
            {
                Guid.NewGuid()
            });
            return retVal;
        }
    }
}

using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.Implementations;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecipeHub.xUnitTests
{
    public class CommentTests
    {
        [Theory]
        [MemberData(nameof(GetCommentsData))]
        public void Comment_testing(Comment comment)
        {
            CommentService cs = new CommentService();
            bool exceptionThrown = false;
            try
            {
                Guid rId1 = Guid.Parse("1a533389 - cc47 - 43d9 - 9294 - 8d39c1ab4888");
                cs.CreateComment(comment, rId1);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> GetCommentsData()
        {
            List<object[]> retVal = new List<object[]>();
            Guid cId1 = Guid.Parse("e235bec1-e7a7-42bb-9460-de56f6420cdf");
            Comment c1 = new Comment(cId1, 5, "This is good recipe");
            retVal.Add(new object[]
            {
                c1
            });

            return retVal;
        }
    }
}

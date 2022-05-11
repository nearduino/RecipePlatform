using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Utilities;
using Shouldly;
using Xunit;

namespace RecipeHub.UnitTests
{
    public class TextValidatorTests
    {
        [Theory]
        [MemberData(nameof(GetCheckNameData))]
        public void check_name_should_return_true(string name, bool shouldBe)
        {
            TextValidator.CheckName(name).ShouldBe(shouldBe);
        }

        public static IEnumerable<object[]> GetCheckNameData()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
            {
                "Vitamin A", true
            });
            retVal.Add(new object[]
            {
                "Vitamin a", true
            });
            retVal.Add(new object[]
            {
                "vitamin a", false
            });
            retVal.Add(new object[]
            {
                "a", false
            });
            retVal.Add(new object[]
            {
                "A", false
            });
            retVal.Add(new object[]
            {
                "Vitamin a a a a aa Vvvv vvv", true
            });
            retVal.Add(new object[]
            {
                "Vitamin a a a a aa Vvvv vvv!", true
            });
            retVal.Add(new object[]
            {
                "Vitamin a a a a aa VVvv vvv", false
            });
            return retVal;
        }
    }
}

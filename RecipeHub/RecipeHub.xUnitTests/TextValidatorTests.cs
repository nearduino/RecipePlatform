using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Utilities;
using Shouldly;
using Xunit;

namespace RecipeHub.xUnitTests
{
    public class TextValidatorTests
    {
        [Theory]
        [MemberData(nameof(GetCheckNameData))]
        public void Check_name_should_return_true(string name, bool shouldBe)
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
                "VitAmin a", false
            });
            retVal.Add(new object[]
            {
                "Vitamin a!", false
            });
            retVal.Add(new object[]
            {
                "Vitamin a a a a aa VVvv vvv", false
            });
            retVal.Add(new object[]
            {
                "Crni mladi luk", true
            });
            retVal.Add(new object[]
            {
                "Beli hleb", true
            });
            retVal.Add(new object[]
            {
                "Rotkvice", true
            });
            retVal.Add(new object[]
            {
                "Suncokretovo ulje", true
            });
            return retVal;
        }
    }
}

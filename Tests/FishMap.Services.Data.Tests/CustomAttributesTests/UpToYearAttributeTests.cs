namespace FishMap.Services.Data.Tests.CustomAttributesTests
{
    using System;

    using FishMap.Common.ValidationAttributes;
    using Xunit;

    public class UpToYearAttributeTests
    {
        [Fact]
        public void IsValidReturnsFalseForDateAfterAYear()
        {
            var attribute = new UpToYearAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(1).AddSeconds(1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsFalseForDateBeforeToday()
        {
            var attribute = new UpToYearAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(-1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateInBetweenNowAndOneYearAhead()
        {
            var attribute = new UpToYearAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddMonths(6));

            Assert.True(isValid);
        }
    }
}

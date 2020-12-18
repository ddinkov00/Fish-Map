namespace FishMap.Services.Data.Tests.CustomAttributesTests
{
    using System;

    using FishMap.Common.ValidationAttributes;
    using Xunit;

    public class WithinThreeYearsAttributeTests
    {
        [Fact]
        public void IsValidReturnsFalseForDateAferToday()
        {
            var attribute = new WithinThreeYearsAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsFalseForDateBoforeMoreThanYears()
        {
            var attribute = new WithinThreeYearsAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddYears(-3).AddDays(-1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateInBetwwenTodayAndThreeYearsBefore()
        {
            var attribute = new WithinThreeYearsAttribute();

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(1));

            Assert.False(isValid);
        }
    }
}

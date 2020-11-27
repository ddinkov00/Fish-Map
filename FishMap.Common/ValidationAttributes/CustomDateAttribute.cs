namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
            : base(
                typeof(DateTime),
                DateTime.Now.AddYears(-5).ToShortDateString(),
                DateTime.Now.ToShortDateString())
        {
        }
    }
}

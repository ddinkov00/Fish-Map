namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TripDateAttribute : RangeAttribute
    {
        public TripDateAttribute()
            : base(
                typeof(DateTime),
                DateTime.Now.AddYears(-5).ToShortDateString(),
                DateTime.Now.ToShortDateString())
        {
        }
    }
}

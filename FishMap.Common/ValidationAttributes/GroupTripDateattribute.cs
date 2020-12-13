namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GroupTripDateAttribute : RangeAttribute
    {
        public GroupTripDateAttribute()
            : base(
                typeof(DateTime),
                DateTime.Now.ToShortDateString(),
                DateTime.Now.AddYears(+1).ToShortDateString())
        {
        }
    }
}

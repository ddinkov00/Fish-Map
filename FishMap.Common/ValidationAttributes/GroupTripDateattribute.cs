namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GroupTripDateattribute : RangeAttribute
    {
        public GroupTripDateattribute()
            : base(
                typeof(DateTime),
                DateTime.Now.AddYears(+1).ToShortDateString(),
                DateTime.Now.ToShortDateString())
        {
        }
    }
}

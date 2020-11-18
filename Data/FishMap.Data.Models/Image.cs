namespace FishMap.Data.Models
{
    using System;

    using FishMap.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Uri { get; set; }

        public int? FishKindId { get; set; }

        public virtual FishSpecies FishKind { get; set; }

        public int? TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}

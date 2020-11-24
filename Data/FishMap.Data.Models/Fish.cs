namespace FishMap.Data.Models
{
    using System.Collections.Generic;

    using FishMap.Data.Common.Models;

    public class Fish : BaseDeletableModel<int>
    {
        public int FishKindId { get; set; }

        public virtual FishSpecies FishKind { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
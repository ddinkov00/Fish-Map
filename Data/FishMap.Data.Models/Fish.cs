namespace FishMap.Data.Models
{
    using System.Collections.Generic;

    using FishMap.Data.Common.Models;

    public class Fish : BaseDeletableModel<int>
    {
        public Fish()
        {
            this.Images = new HashSet<Image>();
        }

        public int FishSpeciesId { get; set; }

        public virtual FishSpecies FishSpecies { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
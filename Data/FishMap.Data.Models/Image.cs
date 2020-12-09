namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishMap.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Url { get; set; }

        public virtual FishSpecies FishSpecies { get; set; }

        public int? TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public int? FishId { get; set; }

        public Fish Fish { get; set; }
    }
}

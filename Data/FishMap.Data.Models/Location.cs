namespace FishMap.Data.Models
{
    using FishMap.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        public float Latitude { get; set; }

        public float Longtitude { get; set; }

        public bool? IsFishingSpot { get; set; }

        public bool? IsMeetingSpot { get; set; }

        public int? TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public int? TownId { get; set; }

        public virtual Town Town { get; set; }

        public int? GroupTripId { get; set; }

        public virtual GroupTrip GroupTrip { get; set; }
    }
}
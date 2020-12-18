namespace FishMap.Data.Models
{

    using FishMap.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int? TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public int? GroupTripId { get; set; }

        public virtual GroupTrip GroupTrip { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}

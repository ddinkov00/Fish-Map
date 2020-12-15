namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class UserGroupTrip : BaseDeletableModel<int>
    {
        [Required]
        public string GuestId { get; set; }

        public virtual ApplicationUser Guest { get; set; }

        public int GroupTripId { get; set; }

        public virtual GroupTrip GroupTrip { get; set; }
    }
}

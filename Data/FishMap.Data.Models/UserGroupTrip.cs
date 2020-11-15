namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserGroupTrip
    {
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int GroupTripId { get; set; }

        public virtual GroupTrip GroupTrip { get; set; }
    }
}

namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserGroupTrip
    {
        public int Id { get; set; }

        [Required]
        public string HostId { get; set; }

        public virtual ApplicationUser Host { get; set; }

        public int GroupTripId { get; set; }

        public virtual GroupTrip GroupTrip { get; set; }
    }
}

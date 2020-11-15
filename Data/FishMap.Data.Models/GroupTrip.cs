namespace FishMap.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class GroupTrip : BaseDeletableModel<int>
    {
        public GroupTrip()
        {
            this.Guests = new HashSet<UserGroupTrip>();
            this.Locations = new HashSet<Location>();
        }

        public DateTime MeetingTime { get; set; }

        public DateTime FishingTime { get; set; }

        [Required]
        [Range(1, 10)]
        public int FreeSeats { get; set; }

        [Required]
        public FishingMethod FishingMethod { get; set; }

        public int TargetFishSpeciesId { get; set; }

        public virtual FishSpecies TargetFishSpecies { get; set; }

        public string HostId { get; set; }

        public virtual ApplicationUser Host { get; set; }

        public virtual ICollection<UserGroupTrip> Guests { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}

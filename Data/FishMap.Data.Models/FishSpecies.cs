namespace FishMap.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class FishSpecies : BaseDeletableModel<int>
    {
        public FishSpecies()
        {
            this.Fish = new HashSet<Fish>();
            this.Trips = new HashSet<Trip>();
            this.GroupTrips = new HashSet<GroupTrip>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCarnivore { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Fish> Fish { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<GroupTrip> GroupTrips { get; set; }
    }
}
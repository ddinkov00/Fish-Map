namespace FishMap.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FishMap.Data.Common.Models;

    public class FishSpecies : BaseDeletableModel<int>
    {
        public FishSpecies()
        {
            this.Fish = new HashSet<Fish>();
            this.GroupTrips = new HashSet<GroupTrip>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsCarnivore { get; set; }

        public int? MinimumLegalSize { get; set; }

        public int ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Fish> Fish { get; set; }

        public virtual ICollection<GroupTrip> GroupTrips { get; set; }
    }
}
namespace FishMap.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Trips = new HashSet<Trip>();
        }

        [Required]
        public string Name { get; set; }

        [Range(-90, 90)]
        public float LocationLatitude { get; set; }

        [Range(-180, 180)]
        public float LocationLongtitude { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}

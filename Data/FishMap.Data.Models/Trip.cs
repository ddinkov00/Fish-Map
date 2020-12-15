namespace FishMap.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class Trip : BaseDeletableModel<int>
    {
        public Trip()
        {
            this.FishCaught = new HashSet<Fish>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string WaterPoolName { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Range(0, 100)]
        public int FishCaughtCount { get; set; }

        [Range(-90, 90)]
        public float LocationLatitude { get; set; }

        [Range(-180, 180)]
        public float LocationLongtitude { get; set; }

        public DateTime Date { get; set; }

        public FishingMethodEnum FishingMethod { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Fish> FishCaught { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

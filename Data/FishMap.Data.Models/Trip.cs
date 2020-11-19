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
            this.Images = new HashSet<Image>();
            this.FishCaught = new HashSet<Fish>();
        }

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

        public FishingMethod FishingMethod { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Fish> FishCaught { get; set; }
    }
}

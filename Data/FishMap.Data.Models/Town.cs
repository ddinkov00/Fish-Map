namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FishMap.Data.Common.Models;

    public class Town : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Range(-90, 90)]
        public float LocationLatitude { get; set; }

        [Range(-180, 180)]
        public float LocationLongtitude { get; set; }
    }
}

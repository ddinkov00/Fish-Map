namespace FishMap.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Common.ValidationAttributes;

    public class CreateTripInputModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Име на воден басейн")]

        public string WaterPoolName { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Range(1, 100)]
        [Display(Name = "Брой хванати риби")]
        public int FishCaughtCout { get; set; }

        [Range(-90, 90)]
        public float LocationLatitude { get; set; }

        [Range(-180, 180)]
        public float LocationLongtitude { get; set; }

        [Display(Name = "Дата")]
        [CustomDateAttribute]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Метод на риболов")]
        public string FishingMethod { get; set; }
    }
}

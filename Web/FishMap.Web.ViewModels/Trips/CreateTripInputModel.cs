namespace FishMap.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

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
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Метод на риболов")]
        public string FishingMethod { get; set; }

        [Required]
        [Display(Name = "Снимки")]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<FishInputModel> Fish { get; set; }
    }
}

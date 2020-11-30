namespace FishMap.Web.ViewModels.Fish
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Web.ViewModels.FishSpecies;
    using Microsoft.AspNetCore.Http;

    public class CreateFishInListInputModel
    {
        [Range(0.001, 1000)]
        [Display(Name = "Тегло на рибата в килограми")]
        public double WeightInKilos { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Дължина на рибата в сантиметри")]
        public double LengthInCentimeters { get; set; }

        [Display(Name = "Вид на рибата")]
        public int FishSpeciesId { get; set; }

        //public IEnumerable<IFormFile> Images { get; set; }
    }
}

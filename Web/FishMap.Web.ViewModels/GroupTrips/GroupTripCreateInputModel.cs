namespace FishMap.Web.ViewModels.GroupTrips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishMap.Common.ValidationAttributes;
    using FishMap.Web.ViewModels.FishSpecies;

    public class GroupTripCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Име на воден басейн")]
        public string WaterPoolName { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Време на тръгване")]
        [UpToYearAttribute]
        public DateTime MeetingTime { get; set; }

        [Range(-90, 90)]
        public float MeetingSpotLatitude { get; set; }

        [Range(-180, 180)]
        public float MeetingSpotLongtitude { get; set; }

        [Display(Name = "Начало на риболовния излет")]
        [UpToYearAttribute]
        public DateTime FishingTime { get; set; }

        [Range(-90, 90)]
        public float FishingSpotLatitude { get; set; }

        [Range(-180, 180)]
        public float FishingSpotLongtitude { get; set; }

        [Range(0, 10)]
        [Display(Name = "Свободни места")]
        public int FreeSeats { get; set; }

        [Display(Name = "Метод на риболов")]
        public int FishingMethodId { get; set; }

        [Display(Name = "Таргетиран вид риба")]
        public int TargetFishSpeciesId { get; set; }

        public IEnumerable<FishSpeciesSelectListModel> FishSpeciesItems { get; set; }
    }
}
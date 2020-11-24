namespace FishMap.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FishInputModel
    {
        [Range(0.001, 1000)]
        public double WeightInKilos { get; set; }

        [Range(1, 1000)]
        public double LengthInCentimeters { get; set; }

        public int FishSpeciesId { get; set; }
    }
}

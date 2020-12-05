namespace FishMap.Web.ViewModels
{
    using System;

    public class TripByFishSpeciesViewModel
    {
        public int FishCaughtCount { get; set; }

        public DateTime Date { get; set; }

        public string CaougtByUserName { get; set; }

        public string FishingMethod { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}

namespace FishMap.Web.ViewModels.Trips
{
    using System;

    public class TripByFishSpeciesViewModel
    {
        public int FishCaughtCount { get; set; }

        public string Date { get; set; }

        public string Email { get; set; }

        public string Username => this.Email.Split("@")[0];

        public string FishingMethod { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}

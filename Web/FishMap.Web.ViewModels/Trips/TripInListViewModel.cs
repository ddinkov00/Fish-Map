namespace FishMap.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;

    public class TripInListViewModel
    {
        public int Id { get; set; }

        public string WaterPoolName { get; set; }

        public string Date { get; set; }

        public IEnumerable<string> CaughtFishSpecies { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public string Username => this.Email.Split('@')[0];

        public int FishCaughtCount { get; set; }

        public string NearestTownName { get; set; }
    }
}

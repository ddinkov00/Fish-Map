using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishMap.Web.ViewModels.Trips
{
    public class TripForMapViewModel
    {
        public int Id { get; set; }

        public float Latitude { get; set; }

        public float Longtitude { get; set; }

        public DateTime Date { get; set; }

        public string DateAsString => $"{this.Date.Day}/{this.Date.Month}/{this.Date.Year}г.";

        public string CreatedByUserEmail { get; set; }

        public string CreatedByUsername => this.CreatedByUserEmail.Split('@').ElementAt(0);

        public string FishingMethod { get; set; }

        public int FishCaughtCount { get; set; }
    }
}

namespace FishMap.Web.ViewModels.Trips
{
    using System.Collections.Generic;

    public class AllTripsForMapViewModel
    {
        public IEnumerable<TripForMapViewModel> Trips { get; set; }
    }
}

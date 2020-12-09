namespace FishMap.Web.ViewModels.Trips
{
    using System.Collections.Generic;

    public class TripListViewModel : PagingViewModel
    {
        public IEnumerable<TripInListViewModel> Trips { get; set; }
    }
}

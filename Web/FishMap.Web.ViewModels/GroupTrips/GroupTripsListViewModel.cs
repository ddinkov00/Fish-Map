namespace FishMap.Web.ViewModels.GroupTrips
{
    using System.Collections.Generic;

    public class GroupTripsListViewModel : PagingViewModel
    {
        public IEnumerable<GroupTripInListViewModel> GroupTrips { get; set; }
    }
}

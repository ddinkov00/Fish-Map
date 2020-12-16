namespace FishMap.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.GroupTrips;

    public interface IGroupTripsService
    {
        Task<int> CreateAsync(GroupTripCreateInputModel inputModel, string userId);

        IEnumerable<GroupTripInListViewModel> GetUpcomingForPaging(int page, int itemsPerPage);

        GroupTripByIdViewModel GetById(int id);

        int GetUpcomingCount();

        Task EnrollUser(int id, string userId);
    }
}

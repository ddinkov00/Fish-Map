namespace FishMap.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.GroupTrips;

    public interface IGroupTripsService
    {
        Task<int> CreateAsync(GroupTripCreateInputModel inputModel, string userId);
    }
}

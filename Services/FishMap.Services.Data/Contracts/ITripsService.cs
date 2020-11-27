namespace FishMap.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Trips;

    public interface ITripsService
    {
        Task<TripsToFishInputModel> CreateAsync(CreateTripInputModel input, string userId);
    }
}

namespace FishMap.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Routing;

    public interface ITripsService
    {
        Task<AddFishRouteData> CreateAsync(CreateTripInputModel input, string userId);
    }
}

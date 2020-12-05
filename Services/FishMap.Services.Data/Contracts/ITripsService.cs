namespace FishMap.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels;
    using FishMap.Web.ViewModels.Trips;

    public interface ITripsService
    {
        Task<AddFishRouteData> CreateAsync(CreateTripInputModel input, string userId);

        IEnumerable<TripByFishSpeciesViewModel> GetAllByFishSpecies(int fishSpeciesId);
    }
}

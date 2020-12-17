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

        IEnumerable<TripInListViewModel> GetAllForPaging(int page, int itemsPerPage);

        int GetAllCount();

        TripByIdViewModel GetById(int id);

        IEnumerable<TripForMapViewModel> GetAllForMap();

        IEnumerable<TripInListViewModel> OrderAllByCreatedOnAsc(int page, int itemsPerPage);

        IEnumerable<TripInListViewModel> OrderAllByCreatedOnDesc(int page, int itemsPerPage);

        IEnumerable<TripInListViewModel> OrderAllByTripDateAsc(int page, int itemsPerPage);

        IEnumerable<TripInListViewModel> OrderAllByTripDateDesc(int page, int itemsPerPage);
    }
}

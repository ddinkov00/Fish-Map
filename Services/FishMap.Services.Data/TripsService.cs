namespace FishMap.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripsRepository;
        private readonly IFishServices fishServices;

        public TripsService(
            IDeletableEntityRepository<Trip> tripsRepository,
            IFishServices fishServices)
        {
            this.tripsRepository = tripsRepository;
            this.fishServices = fishServices;
        }

        public async Task<AddFishRouteData> CreateAsync(CreateTripInputModel input, string userId)
        {
            var trip = new Trip
            {
                WaterPoolName = input.WaterPoolName,
                Description = input.Description,
                FishCaughtCount = input.FishCaughtCout,
                LocationLatitude = input.LocationLatitude,
                LocationLongtitude = input.LocationLongtitude,
                Date = input.Date,
                FishingMethod = (FishingMethodEnum)input.FishingMethod,
                UserId = userId,
            };

            await this.tripsRepository.AddAsync(trip);
            await this.tripsRepository.SaveChangesAsync();

            return new AddFishRouteData
            {
                TripId = trip.Id,
                FishCount = trip.FishCaughtCount,
            };
        }

        public IEnumerable<TripByFishSpeciesViewModel> GetAllByFishSpecies(int fishSpeciesId)
        {
            return this.tripsRepository.AllAsNoTracking()
                .Where(t => t.FishCaught.Any(f => f.FishSpeciesId == fishSpeciesId))
                .Select(t => new TripByFishSpeciesViewModel
                {
                    FishCaughtCount = t.FishCaughtCount,
                    Date = $"{t.Date.Day}/{t.Date.Month}/{t.Date.Year}г.",
                    Email = t.User.Email,
                    FishingMethod = t.FishingMethod.ToString(),
                    Latitude = t.LocationLatitude,
                    Longtitude = t.LocationLongtitude,
                });
        }

        public IEnumerable<TripInListViewModel> GetAllForPaging(int page, int itemsPerPage = 9)
        {
            return this.tripsRepository.AllAsNoTracking()
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(t => new TripInListViewModel
                {
                    Id = t.Id,
                    WaterPoolName = t.WaterPoolName,
                    Date = $"{t.Date.Day}/{t.Date.Month}/{t.Date.Year}г.",
                    CaughtFishSpecies = t.FishCaught
                        .Select(fs => fs.FishSpecies.Name),
                    ImageUrl = t.FishCaught.FirstOrDefault()
                        .Images.FirstOrDefault().Url,
                    Email = t.User.Email,
                    FishCaughtCount = t.FishCaughtCount,
                }).ToList();
        }

        public int GetAllCount()
        {
            return this.tripsRepository
                .AllAsNoTracking()
                .Count();
        }

        public TripByIdViewModel GetById(int id)
        {
            return this.tripsRepository.AllAsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TripByIdViewModel
                {
                    WaterPoolName = t.WaterPoolName,
                    Description = t.Description,
                    Date = $"{t.Date.Day}/{t.Date.Month}/{t.Date.Year} г.",
                    FishingMethod = t.FishingMethod.ToString(),
                    LocationLatitude = t.LocationLatitude,
                    LoacationLongtitude = t.LocationLongtitude,
                    UserEmail = t.User.Email,
                    Fish = this.fishServices.GetAllByTripId(id),
                }).FirstOrDefault();
        }
    }
}

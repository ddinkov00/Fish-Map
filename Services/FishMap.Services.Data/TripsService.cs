namespace FishMap.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Routing;

    public class TripsService : ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripsRepository;

        public TripsService(IDeletableEntityRepository<Trip> tripsRepository)
        {
            this.tripsRepository = tripsRepository;
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
                    CaughtByUserName = t.User.UserName,
                    FishingMethod = t.FishingMethod.ToString(),
                    Latitude = t.LocationLatitude,
                    Longtitude = t.LocationLongtitude,
                });
        }
    }
}

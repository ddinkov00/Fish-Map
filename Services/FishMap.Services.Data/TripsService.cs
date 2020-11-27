namespace FishMap.Services.Data
{
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripsRepository;

        public TripsService(IDeletableEntityRepository<Trip> tripsRepository)
        {
            this.tripsRepository = tripsRepository;
        }

        public async Task<TripsToFishInputModel> CreateAsync(CreateTripInputModel input, string userId)
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

            return new TripsToFishInputModel()
            {
                TripId = trip.Id,
                FishCount = trip.FishCaughtCount,
            };
        }
    }
}

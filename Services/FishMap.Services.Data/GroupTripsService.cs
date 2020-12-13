namespace FishMap.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.GroupTrips;

    public class GroupTripsService : IGroupTripsService
    {
        private readonly IDeletableEntityRepository<GroupTrip> groupTripsRepository;

        public GroupTripsService(IDeletableEntityRepository<GroupTrip> groupTripsRepository)
        {
            this.groupTripsRepository = groupTripsRepository;
        }

        public async Task<int> CreateAsync(GroupTripCreateInputModel inputModel, string userId)
        {
            var groupTrip = new GroupTrip
            {
                WaterPoolName = inputModel.WaterPoolName,
                Description = inputModel.Description,
                FreeSeats = inputModel.FreeSeats,
                MeetingTime = inputModel.MeetingTime,
                MeetingSpotLatitude = inputModel.MeetingSpotLatitude,
                MeetingSpotLongtitude = inputModel.MeetingSpotLongtitude,
                FishingTime = inputModel.FishingTime,
                FishingSpotLatitued = inputModel.FishingSpotLatitude,
                FishingSpotLongtitude = inputModel.FishingSpotLongtitude,
                TargetFishSpeciesId = inputModel.TargetFishSpeciesId,
                FishingMethod = (FishingMethodEnum)inputModel.FishingMethodId,
                HostId = userId,
            };

            await this.groupTripsRepository.AddAsync(groupTrip);
            await this.groupTripsRepository.SaveChangesAsync();

            return groupTrip.Id;
        }
    }
}

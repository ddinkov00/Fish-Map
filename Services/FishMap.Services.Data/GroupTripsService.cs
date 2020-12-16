namespace FishMap.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels;
    using FishMap.Web.ViewModels.GroupTrips;

    public class GroupTripsService : IGroupTripsService
    {
        private readonly ITownsService townsService;
        private readonly IDeletableEntityRepository<GroupTrip> groupTripsRepository;

        public GroupTripsService(
            ITownsService townsService,
            IDeletableEntityRepository<GroupTrip> groupTripsRepository)
        {
            this.townsService = townsService;
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

        public int GetAllCount()
        {
            return this.groupTripsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<GroupTripInListViewModel> GetAllForPaging(int page, int itemsPerPage = 9)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(gt => new GroupTripInListViewModel
                {
                    Id = gt.Id,
                    WaterPoolName = gt.WaterPoolName,
                    TargetFishSecies = gt.TargetFishSpecies.Name,
                    HostEmail = gt.Host.UserName,
                    GuestsCount = gt.Guests.Count,
                    AllSeats = gt.FreeSeats,
                    TripDate = $"{gt.FishingTime.Day}/{gt.FishingTime.Month}/{gt.FishingTime.Year}г.",
                    NearestCity = this.townsService.GetNearestCity(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
                }).ToList();
        }

        public GroupTripByIdViewModel GetById(int id)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .Where(gt => gt.Id == id)
                .Select(gt => new GroupTripByIdViewModel
                {
                    Id = gt.Id,
                    WaterPoolName = gt.WaterPoolName,
                    Description = gt.Description,
                    FishingDate = $"{gt.FishingTime.Day}/{gt.FishingTime.Month}/{gt.FishingTime.Year}г. " +
                        $"- {gt.FishingTime.TimeOfDay}",
                    FishingLatitude = gt.FishingSpotLatitued,
                    FishingLongtitude = gt.FishingSpotLongtitude,
                    MeetingDate = $"{gt.MeetingTime.Day}/{gt.MeetingTime.Month}/{gt.MeetingTime.Year}г." +
                        $" - {gt.MeetingTime.TimeOfDay}",
                    MeetingLatitude = gt.MeetingSpotLatitude,
                    MeetingLongtitude = gt.MeetingSpotLongtitude,
                    FishingMethod = gt.FishingMethod.ToString(),
                    TargetFishSpecies = gt.TargetFishSpecies.Name,
                    HostEmail = gt.Host.UserName,
                    GuestsCount = gt.Guests.Count(),
                    AllSeats = gt.FreeSeats,
                    Guests = gt.Guests
                        .Select(x => new UserForGroupTripByIdViewModel
                        {
                            Id = x.GuestId,
                            Email = x.Guest.Email,
                        }),
                }).FirstOrDefault();
        }
    }
}

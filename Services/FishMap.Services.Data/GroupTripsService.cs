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
    using FishMap.Web.ViewModels.Comments;
    using FishMap.Web.ViewModels.GroupTrips;

    public class GroupTripsService : IGroupTripsService
    {
        private readonly ITownsService townsService;
        private readonly IDeletableEntityRepository<GroupTrip> groupTripsRepository;
        private readonly IDeletableEntityRepository<UserGroupTrip> userGroupTripsRepository;

        public GroupTripsService(
            ITownsService townsService,
            IDeletableEntityRepository<GroupTrip> groupTripsRepository,
            IDeletableEntityRepository<UserGroupTrip> userGroupTripsRepository)
        {
            this.townsService = townsService;
            this.groupTripsRepository = groupTripsRepository;
            this.userGroupTripsRepository = userGroupTripsRepository;
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

        public int GetUpcomingCount()
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .Where(gt => gt.FishingTime > DateTime.Now)
                .Count();
        }

        public IEnumerable<GroupTripInListViewModel> GetUpcomingForPaging(int page, int itemsPerPage = 9)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .Where(gt => gt.FishingTime > DateTime.Now)
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
                    NearestCity = this.townsService.GetNearestTownName(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
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
                    Comments = gt.Comments
                        .Select(c => new CommentViewModel
                        {
                            Content = c.Content,
                            AddedByUserEmail = c.User.Email,
                            CreatedOn = c.CreatedOn,
                        }),
                }).FirstOrDefault();
        }

        public async Task EnrollUser(int id, string userId)
        {
            var groupTrip = this.groupTripsRepository.All()
                .Where(gt => gt.Id == id)
                .FirstOrDefault();

            if (this.userGroupTripsRepository.AllAsNoTracking().Any(x => x.GroupTripId == id && x.GuestId == userId))
            {
                throw new OperationCanceledException("Вече сте записани за този излет!");
            }

            if (groupTrip.HostId == userId)
            {
                throw new OperationCanceledException("Не може да се запишете за излет създаден от вас!");
            }

            if (groupTrip.Guests.Count() + 1 > groupTrip.FreeSeats)
            {
                throw new OperationCanceledException("Всички места за този излет вече са заети!");
            }

            var userGroupTrip = new UserGroupTrip
            {
                GuestId = userId,
                GroupTripId = id,
            };

            groupTrip.Guests.Add(userGroupTrip);
            await this.groupTripsRepository.SaveChangesAsync();
        }

        public bool IsUserCreator(string userId, int groupTripId)
        {
            var tripUserId = this.groupTripsRepository.All()
                .Where(t => t.Id == groupTripId)
                .Select(t => t.HostId)
                .FirstOrDefault();

            return tripUserId == userId;
        }

        public async Task Delete(int groupTripId)
        {
            var trip = this.groupTripsRepository.All()
                .Where(t => t.Id == groupTripId)
                .FirstOrDefault();

            this.groupTripsRepository.Delete(trip);
            await this.groupTripsRepository.SaveChangesAsync();
        }

        public IEnumerable<GroupTripInListViewModel> OrderUpcomingByCreatedOnAsc(int page, int itemsPerPage)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .OrderBy(gt => gt.CreatedOn)
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
                    NearestCity = this.townsService.GetNearestTownName(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
                }).ToList();
        }

        public IEnumerable<GroupTripInListViewModel> OrderUpcomingByCreatedOnDesc(int page, int itemsPerPage)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .OrderByDescending(gt => gt.CreatedOn)
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
                    NearestCity = this.townsService.GetNearestTownName(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
                }).ToList();
        }

        public IEnumerable<GroupTripInListViewModel> OrderUpcomingByTripDateAsc(int page, int itemsPerPage)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .OrderBy(gt => gt.MeetingTime)
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
                    NearestCity = this.townsService.GetNearestTownName(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
                }).ToList();
        }

        public IEnumerable<GroupTripInListViewModel> OrderUpcomingByTripDateDesc(int page, int itemsPerPage)
        {
            return this.groupTripsRepository.AllAsNoTracking()
                .OrderByDescending(gt => gt.MeetingTime)
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
                    NearestCity = this.townsService.GetNearestTownName(gt.FishingSpotLatitued, gt.FishingSpotLongtitude),
                }).ToList();
        }
    }
}

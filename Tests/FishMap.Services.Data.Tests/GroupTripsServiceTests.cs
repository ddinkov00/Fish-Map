namespace FishMap.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Web.ViewModels.GroupTrips;
    using Moq;
    using Xunit;

    public class GroupTripsServiceTests
    {
        [Fact]
        public async Task EnrollingUserTwoTimesToThesameTripShoudThrowAnError()
        {
            var groupTripslist = new List<GroupTrip>
            {
                new GroupTrip
                {
                    Id = 1,
                    HostId = "2",
                    FreeSeats = 3,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>
            {
                new UserGroupTrip
                {
                    Id = 1,
                    GuestId = "1",
                    GroupTripId = 1,
                },
            };

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();
            mockUserGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(userGroupTripsList.AsQueryable());

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.AddAsync(It.IsAny<GroupTrip>())).Callback((GroupTrip groupTrip) => groupTripslist.Add(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await service.EnrollUser(1, "1"));
            var message = "Вече сте записани за този излет!";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task EnrollingUserTwoTimesToGroupTripWhichHeCreatedShoudThrowException()
        {
            var groupTripslist = new List<GroupTrip>
            {
                new GroupTrip
                {
                    Id = 1,
                    HostId = "1",
                    FreeSeats = 3,
                },
            };

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.AddAsync(It.IsAny<GroupTrip>())).Callback((GroupTrip groupTrip) => groupTripslist.Add(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await service.EnrollUser(1, "1"));
            var message = "Не може да се запишете за излет създаден от вас!";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task EnrollingUserToGroupTripWhithNoFreeSeatsShoudThrowException()
        {
            var groupTripslist = new List<GroupTrip>
            {
                new GroupTrip
                {
                    Id = 1,
                    HostId = "2",
                    FreeSeats = 0,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>
            {
                new UserGroupTrip
                {
                    Id = 1,
                    GuestId = "1",
                    GroupTripId = 1,
                },
            };

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.AddAsync(It.IsAny<GroupTrip>())).Callback((GroupTrip groupTrip) => groupTripslist.Add(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await service.EnrollUser(1, "1"));
            var message = "Всички места за този излет вече са заети!";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task EnrollingUserToGroupTripShoudIncreaseGuestsCount()
        {
            var groupTripslist = new List<GroupTrip>
            {
                new GroupTrip
                {
                    Id = 1,
                    HostId = "2",
                    FreeSeats = 3,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();
            mockUserGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(userGroupTripsList.AsQueryable());

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.AddAsync(It.IsAny<GroupTrip>())).Callback((GroupTrip groupTrip) => groupTripslist.Add(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            await service.EnrollUser(1, "3");
            Assert.Single(groupTripslist.ElementAt(0).Guests);
        }

        [Fact]
        public async Task CreatingGroupTripShouldIncreaseGroupTripsCount()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                },
                new GroupTrip
                {
                    Id = 2,
                },
                new GroupTrip
                {
                    Id = 3,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();
            mockUserGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(userGroupTripsList.AsQueryable());

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.AddAsync(It.IsAny<GroupTrip>())).Callback((GroupTrip groupTrip) => groupTripslist.Add(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var groupTrip = new GroupTripCreateInputModel
            {
                WaterPoolName = "река Марица",
                Description = "асдасдасдадс",
                MeetingTime = DateTime.UtcNow.AddDays(1),
                MeetingSpotLatitude = 1,
                MeetingSpotLongtitude = 1,
                FishingTime = DateTime.UtcNow.AddDays(2),
                FishingSpotLatitude = 2,
                FishingSpotLongtitude = 2,
                FreeSeats = 2,
                FishingMethodId = 3,
                TargetFishSpeciesId = 5,
            };

            await service.CreateAsync(groupTrip, "1");
            Assert.Equal(4, groupTripslist.Count());
        }

        [Fact]
        public void GetUpcomingForPagingShouldReturnNoMoreThanItemsPerPage()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 2,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 3,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 4,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var itemsPerPage = 3;
            Assert.Equal(itemsPerPage, service.GetUpcomingForPaging(1, itemsPerPage).Count());
        }

        [Fact]
        public void GetCountShouldReturnTheCorrectNumber()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                },
                new GroupTrip
                {
                    Id = 2,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                },
                new GroupTrip
                {
                    Id = 3,
                    FishingTime = DateTime.UtcNow.AddDays(-3),
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            Assert.Equal(2, service.GetUpcomingCount());
        }

        [Fact]
        public void GetByIdShouldReturnTheCorrectUser()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 2,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
                new GroupTrip
                {
                    Id = 3,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var groupTripToGetId = 1;
            Assert.Equal(groupTripToGetId, service.GetById(groupTripToGetId).Id);
        }

        [Fact]
        public void IsUserCreatorShoudReturnTheRightTrueIfUserIsCreatroAndFalseIfNot()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    HostId = "1",
                },
                new GroupTrip
                {
                    Id = 2,
                    HostId = "2",
                },
                new GroupTrip
                {
                    Id = 3,
                    HostId = "3",
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            Assert.True(service.IsUserCreator("2", 2));
            Assert.False(service.IsUserCreator("2", 1));
        }

        [Fact]
        public async Task DeleteShouldReduceTheCountOfGroupTripsByOne()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                },
                new GroupTrip
                {
                    Id = 2,
                },
                new GroupTrip
                {
                    Id = 3,
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.All()).Returns(groupTripslist.AsQueryable());
            mockGroupTripRepo.Setup(x => x.Delete(It.IsAny<GroupTrip>()))
                .Callback((GroupTrip groupTrip) => groupTripslist.Remove(groupTrip));

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var beforeDeleteCount = groupTripslist.Count();
            await service.Delete(2);

            Assert.Equal(beforeDeleteCount - 1, groupTripslist.Count());
        }

        [Fact]
        public void OrderUpcomingByCreatedOnAscShouldReturnGroupTripsOrderdFromOldestToNewest()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    CreatedOn = DateTime.UtcNow.AddDays(2),
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    CreatedOn = DateTime.UtcNow.AddDays(1),
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var unorderedFirsItem = groupTripslist.ElementAt(0).Id;
            var orderedList = service.OrderUpcomingByCreatedOnAsc(1, 3);
            var orderedFirsItem = groupTripslist.ElementAt(1).Id;

            Assert.Equal(unorderedFirsItem, orderedFirsItem);
        }

        [Fact]
        public void OrderUpcomingByCreatedOnDescShouldReturnGroupTripsOrderdFromNewestToOldest()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    CreatedOn = DateTime.UtcNow.AddDays(1),
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    CreatedOn = DateTime.UtcNow.AddDays(2),
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var unorderedFirsItem = groupTripslist.ElementAt(0).Id;
            var orderedList = service.OrderUpcomingByCreatedOnDesc(1, 3);
            var orderedFirsItem = groupTripslist.ElementAt(1).Id;

            Assert.Equal(unorderedFirsItem, orderedFirsItem);
        }

        [Fact]
        public void OrderUpcomingByTripDateAscShouldReturnGroupTripsOrderdFromOldestToNewest()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    MeetingTime = DateTime.UtcNow.AddDays(2),
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    MeetingTime = DateTime.UtcNow.AddDays(1),
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var unorderedFirsItem = groupTripslist.ElementAt(0).Id;
            var orderedList = service.OrderUpcomingByTripDateAsc(1, 3);
            var orderedFirsItem = groupTripslist.ElementAt(1).Id;

            Assert.Equal(unorderedFirsItem, orderedFirsItem);
        }

        [Fact]
        public void OrderUpcomingByTripDateDescShouldReturnGroupTripsOrderdFromOldestToNewest()
        {
            var groupTripslist = new List<GroupTrip>()
            {
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    MeetingTime = DateTime.UtcNow.AddDays(1),
                },
                new GroupTrip
                {
                    Id = 1,
                    WaterPoolName = "Maritsa",
                    TargetFishSpecies = new FishSpecies
                    {
                        Name = "Kefal",
                    },
                    Host = new ApplicationUser
                    {
                        UserName = "Gosho",
                    },
                    Guests = new List<UserGroupTrip>
                    {
                        new UserGroupTrip
                        {
                            Id = 1,
                        },
                        new UserGroupTrip
                        {
                            Id = 2,
                        },
                    },
                    FreeSeats = 4,
                    FishingTime = DateTime.UtcNow.AddDays(1),
                    FishingSpotLatitued = 1,
                    FishingSpotLongtitude = 1,
                    MeetingTime = DateTime.UtcNow.AddDays(2),
                },
            };

            var userGroupTripsList = new List<UserGroupTrip>();

            var mockTownRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(mockTownRepo.Object);

            var mockUserGroupTripRepo = new Mock<IDeletableEntityRepository<UserGroupTrip>>();

            var mockGroupTripRepo = new Mock<IDeletableEntityRepository<GroupTrip>>();
            mockGroupTripRepo.Setup(x => x.AllAsNoTracking()).Returns(groupTripslist.AsQueryable());

            var service = new GroupTripsService(townService, mockGroupTripRepo.Object, mockUserGroupTripRepo.Object);

            var unorderedFirsItem = groupTripslist.ElementAt(0).Id;
            var orderedList = service.OrderUpcomingByTripDateDesc(1, 3);
            var orderedFirsItem = groupTripslist.ElementAt(1).Id;

            Assert.Equal(unorderedFirsItem, orderedFirsItem);
        }
    }
}

namespace FishMap.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Web.ViewModels.Trips;
    using Moq;
    using Xunit;

    public class TripServiceTests
    {
        [Fact]
        public async Task CreateAsynShoudIncreaseReposCountByOne()
        {
            var tripList = new List<Trip>();

            var townsList = new List<Town>
            {
                new Town
                {
                    Id = 1,
                    LocationLatitude = 3,
                    LocationLongtitude = 2,
                },
                new Town
                {
                    Id = 2,
                    LocationLatitude = 5,
                    LocationLongtitude = 1,
                },
                new Town
                {
                    Id = 3,
                    LocationLatitude = 8,
                    LocationLongtitude = 9,
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AddAsync(It.IsAny<Trip>()))
                .Callback((Trip trip) => tripList.Add(trip));

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            townRepo.Setup(x => x.AllAsNoTracking()).Returns(townsList.AsQueryable());
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var tripToAdd = new CreateTripInputModel
            {
                WaterPoolName = "asdfasdf",
                Description = "asdfasfda",
                FishCaughtCout = 2,
                LocationLatitude = 3f,
                LocationLongtitude = 4f,
                Date = DateTime.UtcNow,
                FishingMethod = 1,
            };

            var repoCountBefore = tripList.Count();
            await tripService.CreateAsync(tripToAdd, "2");
            Assert.Equal(repoCountBefore + 1, tripList.Count);
        }

        [Fact]
        public void GetAllByFishSpeciesShoudOnlyReturnTripsWithTheGivvenFishSpeciesId()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    FishCaughtCount = 2,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdas@addsa.bg",
                    },
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 3,
                    LocationLongtitude = 2,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 1,
                        },
                        new Fish
                        {
                            FishSpeciesId = 2,
                        },
                    },
                },
                new Trip
                {
                    Id = 2,
                    FishCaughtCount = 2,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdas@addsa.bg",
                    },
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 3,
                    LocationLongtitude = 2,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 5,
                        },
                        new Fish
                        {
                            FishSpeciesId = 2,
                        },
                    },
                },
                new Trip
                {
                    Id = 3,
                    FishCaughtCount = 2,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdas@addsa.bg",
                    },
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 3,
                    LocationLongtitude = 2,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 1,
                        },
                        new Fish
                        {
                            FishSpeciesId = 2,
                        },
                    },
                },
                new Trip
                {
                    Id = 4,
                    FishCaughtCount = 2,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdas@addsa.bg",
                    },
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 3,
                    LocationLongtitude = 2,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 3,
                        },
                        new Fish
                        {
                            FishSpeciesId = 3,
                        },
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var tripsFromService = tripService.GetAllByFishSpecies(1);
            Assert.Equal(2, tripsFromService.Count());
        }

        [Fact]
        public async Task GetAllCountShoudReturnTheCountOfAlltripsInTheRepo()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                },
                new Trip
                {
                    Id = 2,
                },
                new Trip
                {
                    Id = 3,
                },
                new Trip
                {
                    Id = 4,
                },
                new Trip
                {
                    Id = 5,
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var listCount = tripList.Count();
            var countFromService = tripService.GetAllCount();

            Assert.Equal(listCount, countFromService);
        }

        [Fact]
        public void GetAllForPagingShouldReturnNoMoreThanItemsPerPage()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "a",
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "b",
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "c",
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 4,
                    WaterPoolName = "d",
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var itemsPerPage = 3;

            var tripsFromService = tripService.GetAllForPaging(1, itemsPerPage);
            Assert.Equal(itemsPerPage, tripsFromService.Count());

            tripsFromService = tripService.GetAllForPaging(2, itemsPerPage);
            Assert.Equal(tripList.Count - itemsPerPage, tripsFromService.Count());
        }

        [Fact]
        public void GetByIdShoudReturnTheCorrectTrip()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "dasdas",
                    Description = "asdasdasd",
                    Date = DateTime.UtcNow,
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 1,
                    LocationLongtitude = 1,
                    User = new ApplicationUser
                    {
                        Email = "asdasda@asd.bg",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "dasdas",
                    Description = "asdasdasd",
                    Date = DateTime.UtcNow,
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 1,
                    LocationLongtitude = 1,
                    User = new ApplicationUser
                    {
                        Email = "asdasda@asd.bg",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "dasdas",
                    Description = "asdasdasd",
                    Date = DateTime.UtcNow,
                    FishingMethod = FishingMethodEnum.Spinning,
                    LocationLatitude = 1,
                    LocationLongtitude = 1,
                    User = new ApplicationUser
                    {
                        Email = "asdasda@asd.bg",
                    },
                },
            };

            var commentList = new List<Comment>
            {
                new Comment
                {
                    TripId = 1,
                    CreatedOn = DateTime.UtcNow,
                    Content = "asdasdasdas",
                    User = new ApplicationUser
                    {
                        Email = "asdasd@sds.bg",
                    },
                },
                new Comment
                {
                    TripId = 2,
                    CreatedOn = DateTime.UtcNow,
                    Content = "asdasdasdas",
                    User = new ApplicationUser
                    {
                        Email = "asdasd@sds.bg",
                    },
                },
                new Comment
                {
                    TripId = 3,
                    CreatedOn = DateTime.UtcNow,
                    Content = "asdasdasdas",
                    User = new ApplicationUser
                    {
                        Email = "asdasd@sds.bg",
                    },
                },
            };

            var fishList = new List<Fish>
            {
                new Fish
                {
                    TripId = 1,
                    FishSpecies = new FishSpecies
                    {
                        Name = "Sharan",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdfasdfasdfad",
                        },
                        new Image
                        {
                            Url = "ijfdnvidnfvin",
                        },
                    },
                },
                new Fish
                {
                    TripId = 2,
                    FishSpecies = new FishSpecies
                    {
                        Name = "Sharan",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdfasdfasdfad",
                        },
                        new Image
                        {
                            Url = "ijfdnvidnfvin",
                        },
                    },
                },
                new Fish
                {
                    TripId = 3,
                    FishSpecies = new FishSpecies
                    {
                        Name = "Sharan",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdfasdfasdfad",
                        },
                        new Image
                        {
                            Url = "ijfdnvidnfvin",
                        },
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            commentRepo.Setup(x => x.All())
                .Returns(commentList.AsQueryable());
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            fishRepo.Setup(x => x.AllAsNoTracking())
                .Returns(fishList.AsQueryable());
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var tripFromService = tripService.GetById(2);
            Assert.Equal(tripFromService.Id, tripList.ElementAt(1).Id);
        }

        [Fact]
        public void GetAllFromMapShouldReturnAllTrips()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    LocationLatitude = 2,
                    LocationLongtitude = 6,
                    Date = DateTime.UtcNow,
                    FishingMethod = FishingMethodEnum.Spinning,
                    User = new ApplicationUser
                    {
                        Email = "asdasdsa@we.bg",
                    },
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            Id = 1,
                        },
                        new Fish
                        {
                            Id = 2,
                        },
                        new Fish
                        {
                            Id = 3,
                        },
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var listCount = tripList.Count;
            var tripsFromServiceCount = tripService.GetAllForMap().Count();

            Assert.Equal(listCount, tripsFromServiceCount);
        }

        [Fact]
        public void OrderAllByCreatedOnAscShouldReturnTripsInTheRightOrder()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "a",
                    CreatedOn = DateTime.UtcNow.AddDays(2),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "b",
                    CreatedOn = DateTime.UtcNow.AddDays(1),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "c",
                    CreatedOn = DateTime.UtcNow.AddDays(3),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var itemsPerPage = 3;

            var tripsFromService = tripService.OrderAllByCreatedOnAsc(1, itemsPerPage);

            Assert.Collection(
                tripsFromService,
                item => Assert.Equal(tripList.ElementAt(1).Id, tripsFromService.ElementAt(0).Id),
                item => Assert.Equal(tripList.ElementAt(0).Id, tripsFromService.ElementAt(1).Id),
                item => Assert.Equal(tripList.ElementAt(2).Id, tripsFromService.ElementAt(2).Id));
        }

        [Fact]
        public void OrderAllByCreatedOnDescShouldReturnTripsInTheRightOrder()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "a",
                    CreatedOn = DateTime.UtcNow.AddDays(2),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "b",
                    CreatedOn = DateTime.UtcNow.AddDays(1),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "c",
                    CreatedOn = DateTime.UtcNow.AddDays(3),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var itemsPerPage = 3;

            var tripsFromService = tripService.OrderAllByCreatedOnDesc(1, itemsPerPage);

            Assert.Collection(
                tripsFromService,
                item => Assert.Equal(tripList.ElementAt(1).Id, tripsFromService.ElementAt(2).Id),
                item => Assert.Equal(tripList.ElementAt(0).Id, tripsFromService.ElementAt(1).Id),
                item => Assert.Equal(tripList.ElementAt(2).Id, tripsFromService.ElementAt(0).Id));
        }

        [Fact]
        public void OrderAllByTripDateAscAscShouldReturnTripsInTheRightOrder()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "a",
                    Date = DateTime.UtcNow.AddDays(2),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "b",
                    Date = DateTime.UtcNow.AddDays(1),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "c",
                    Date = DateTime.UtcNow.AddDays(3),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var itemsPerPage = 3;

            var tripsFromService = tripService.OrderAllByTripDateAsc(1, itemsPerPage);

            Assert.Collection(
                tripsFromService,
                item => Assert.Equal(tripList.ElementAt(1).Id, tripsFromService.ElementAt(0).Id),
                item => Assert.Equal(tripList.ElementAt(0).Id, tripsFromService.ElementAt(1).Id),
                item => Assert.Equal(tripList.ElementAt(2).Id, tripsFromService.ElementAt(2).Id));
        }

        [Fact]
        public void OrderAllByTripDateDescShouldReturnTripsInTheRightOrder()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    WaterPoolName = "a",
                    Date = DateTime.UtcNow.AddDays(2),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 2,
                    WaterPoolName = "b",
                    Date = DateTime.UtcNow.AddDays(1),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
                new Trip
                {
                    Id = 3,
                    WaterPoolName = "c",
                    Date = DateTime.UtcNow.AddDays(3),
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "adsfasdff",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "fsafasdfasfda",
                                },
                                new Image
                                {
                                    Url = "wegtsfdgw",
                                },
                            },
                        },
                        new Fish
                        {
                            FishSpecies = new FishSpecies
                            {
                                Name = "fasdfasfda",
                            },
                            Images = new List<Image>
                            {
                                new Image
                                {
                                    Url = "ferhwrsgd",
                                },
                                new Image
                                {
                                    Url = "ktyukbstvg",
                                },
                            },
                        },
                    },
                    User = new ApplicationUser
                    {
                        Email = "asdasd@adasd.bg",
                    },
                    FishCaughtCount = 2,
                    NearestTown = new Town
                    {
                        Name = "Plovdiv",
                    },
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.AllAsNoTracking())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);
            tripRepo.Setup(x => x.AllAsNoTracking()).Returns(tripList.AsQueryable());

            var itemsPerPage = 3;

            var tripsFromService = tripService.OrderAllByTripDateDesc(1, itemsPerPage);

            Assert.Collection(
                tripsFromService,
                item => Assert.Equal(tripList.ElementAt(1).Id, tripsFromService.ElementAt(2).Id),
                item => Assert.Equal(tripList.ElementAt(0).Id, tripsFromService.ElementAt(1).Id),
                item => Assert.Equal(tripList.ElementAt(2).Id, tripsFromService.ElementAt(0).Id));
        }

        [Fact]
        public void IsUserCreatorShuldReturnTrueIfHeCreatedTheTripAndFalseIdNot()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    UserId = "1",
                },
                new Trip
                {
                    Id = 2,
                    UserId = "2",
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.All())
                .Returns(tripList.AsQueryable());

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);

            Assert.True(tripService.IsUserCreator("1", 1));
            Assert.False(tripService.IsUserCreator("2", 1));
        }

        [Fact]
        public async Task DeleteShouldReduceTheRepoCountByOne()
        {
            var tripList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                },
                new Trip
                {
                    Id = 2,
                },
                new Trip
                {
                    Id = 3,
                },
            };

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var tripRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripRepo.Setup(x => x.All())
                .Returns(tripList.AsQueryable());
            tripRepo.Setup(x => x.Delete(It.IsAny<Trip>()))
                .Callback((Trip trip) => tripList.Remove(trip));

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripService = new TripsService(commentService, tripRepo.Object, fishService, townService);

            var listCount = tripList.Count;

            await tripService.Delete(1);
            var listCountAfterService = tripList.Count;
            Assert.Equal(listCount - 1, listCountAfterService);

            await tripService.Delete(3);
            listCountAfterService = tripList.Count;
            Assert.Equal(listCount - 2, listCountAfterService);
        }
    }
}

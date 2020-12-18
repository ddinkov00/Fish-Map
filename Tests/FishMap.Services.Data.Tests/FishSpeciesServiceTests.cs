namespace FishMap.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using Moq;
    using Xunit;

    public class FishSpeciesServiceTests
    {
        [Fact]
        public void GetAllCountShoudReturnTheCountOfAllFishSpeciesInTheRepo()
        {
            var fishSpeciesList = new List<FishSpecies>
            {
                new FishSpecies
                {
                    Id = 1,
                },
                new FishSpecies
                {
                    Id = 2,
                },
                new FishSpecies
                {
                    Id = 3,
                },
            };

            var fishSpeciesRepo = new Mock<IDeletableEntityRepository<FishSpecies>>();
            fishSpeciesRepo.Setup(x => x.AllAsNoTracking()).Returns(fishSpeciesList.AsQueryable());

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripsRepo = new Mock<IDeletableEntityRepository<Trip>>();
            var tripsService = new TripsService(commentService, tripsRepo.Object, fishService, townService);

            var fishSpeciesService = new FishSpeciesService(fishSpeciesRepo.Object, tripsService);

            var fishSpeciesCount = fishSpeciesList.Count;
            var fishSpeciesFountFromService = fishSpeciesService.GetAllCount();

            Assert.Equal(fishSpeciesFountFromService, fishSpeciesCount);
        }

        [Fact]
        public void GetAllForPagingShoudReturnTheFirsItemsPerPageFishSpecies()
        {
            var fishSpeciesList = new List<FishSpecies>
            {
                new FishSpecies
                {
                    Id = 1,
                    Name = "Sharan",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa", 
                },
                new FishSpecies
                {
                    Id = 2,
                    Name = "Sharan",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
                new FishSpecies
                {
                    Id = 3,
                    Name = "Sharan",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
                new FishSpecies
                {
                    Id = 4,
                    Name = "Sharan",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
            };

            var fishSpeciesRepo = new Mock<IDeletableEntityRepository<FishSpecies>>();
            fishSpeciesRepo.Setup(x => x.AllAsNoTracking()).Returns(fishSpeciesList.AsQueryable());

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripsRepo = new Mock<IDeletableEntityRepository<Trip>>();
            var tripsService = new TripsService(commentService, tripsRepo.Object, fishService, townService);

            var fishSpeciesService = new FishSpeciesService(fishSpeciesRepo.Object, tripsService);

            var itemsPerPage = 3;

            var fishSpeciesFromService = fishSpeciesService.GetAllForPaging(1, itemsPerPage);

            Assert.Collection(
                fishSpeciesFromService,
                item => Assert.Equal(fishSpeciesList.ElementAt(0).Id, fishSpeciesFromService.ElementAt(0).Id),
                item => Assert.Equal(fishSpeciesList.ElementAt(1).Id, fishSpeciesFromService.ElementAt(1).Id),
                item => Assert.Equal(fishSpeciesList.ElementAt(2).Id, fishSpeciesFromService.ElementAt(2).Id));

            var fishSpeciesFromServiceSecondPage = fishSpeciesService.GetAllForPaging(2, itemsPerPage);

            Assert.Collection(
                fishSpeciesFromServiceSecondPage,
                item => Assert.Equal(fishSpeciesList.ElementAt(3).Id, fishSpeciesFromServiceSecondPage.ElementAt(0).Id));

            Assert.Single(fishSpeciesFromServiceSecondPage);
        }

        [Fact]
        public void GetAllForSelectListShouldReturnAllFishspeciesAndOrderThemAlphabeticllyByName()
        {
            var fishSpeciesList = new List<FishSpecies>
            {
                new FishSpecies
                {
                    Id = 1,
                    Name = "b",
                },
                new FishSpecies
                {
                    Id = 2,
                    Name = "a",
                },
                new FishSpecies
                {
                    Id = 3,
                    Name = "d",
                },
                new FishSpecies
                {
                    Id = 4,
                    Name = "c",
                },
            };

            var fishSpeciesRepo = new Mock<IDeletableEntityRepository<FishSpecies>>();
            fishSpeciesRepo.Setup(x => x.AllAsNoTracking()).Returns(fishSpeciesList.AsQueryable());

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripsRepo = new Mock<IDeletableEntityRepository<Trip>>();
            var tripsService = new TripsService(commentService, tripsRepo.Object, fishService, townService);

            var fishSpeciesService = new FishSpeciesService(fishSpeciesRepo.Object, tripsService);

            var fishSpeciesFromService = fishSpeciesService.GetAllForSelectList();

            Assert.Equal(fishSpeciesList.Count, fishSpeciesFromService.Count());
            Assert.Collection(
                 fishSpeciesFromService,
                 item => Assert.Equal(fishSpeciesList.ElementAt(1).Name, item.Name),
                 item => Assert.Equal(fishSpeciesList.ElementAt(0).Name, item.Name),
                 item => Assert.Equal(fishSpeciesList.ElementAt(3).Name, item.Name),
                 item => Assert.Equal(fishSpeciesList.ElementAt(2).Name, item.Name));
        }

        [Fact]
        public void GetByIdShoudReturntTheCorrectFishSpecies()
        {
            var fishSpeciesList = new List<FishSpecies>
            {
                new FishSpecies
                {
                    Id = 1,
                    Name = "Sharan",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
                new FishSpecies
                {
                    Id = 2,
                    Name = "Karakuda",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
                new FishSpecies
                {
                    Id = 3,
                    Name = "Pystyrva",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
                new FishSpecies
                {
                    Id = 4,
                    Name = "Kefal",
                    Image = new Image
                    {
                        Url = "afaefasdvawvadsv",
                    },
                    IsCarnivore = true,
                    Description = "afsdfqwefavasfa",
                },
            };

            var tripsList = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    FishCaughtCount = 3,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdasd@asdas.gb",
                    },
                    LocationLatitude = 23f,
                    LocationLongtitude = 25f,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 4,
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
                    FishCaughtCount = 3,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdasd@asdas.gb",
                    },
                    LocationLatitude = 23f,
                    LocationLongtitude = 25f,
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
                    Id = 3,
                    FishCaughtCount = 3,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdasd@asdas.gb",
                    },
                    LocationLatitude = 23f,
                    LocationLongtitude = 25f,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 4,
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
                    FishCaughtCount = 3,
                    Date = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Email = "asdasd@asdas.gb",
                    },
                    LocationLatitude = 23f,
                    LocationLongtitude = 25f,
                    FishCaught = new List<Fish>
                    {
                        new Fish
                        {
                            FishSpeciesId = 4,
                        },
                        new Fish
                        {
                            FishSpeciesId = 1,
                        },
                    },
                },
            };

            var fishSpeciesRepo = new Mock<IDeletableEntityRepository<FishSpecies>>();
            fishSpeciesRepo.Setup(x => x.AllAsNoTracking()).Returns(fishSpeciesList.AsQueryable());

            var commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
            var commentService = new CommentService(commentRepo.Object);

            var fishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            var fishService = new FishServices(fishRepo.Object);

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            var townService = new TownsService(townRepo.Object);

            var tripsRepo = new Mock<IDeletableEntityRepository<Trip>>();
            tripsRepo.Setup(x => x.AllAsNoTracking()).Returns(tripsList.AsQueryable());
            var tripsService = new TripsService(commentService, tripsRepo.Object, fishService, townService);

            var fishSpeciesService = new FishSpeciesService(fishSpeciesRepo.Object, tripsService);

            var fishSpeciesFromService = fishSpeciesService.GetById(2);
            Assert.Equal(fishSpeciesFromService.Name, fishSpeciesList.ElementAt(1).Name);

            fishSpeciesFromService = fishSpeciesService.GetById(4);
            Assert.Equal(fishSpeciesFromService.Name, fishSpeciesList.ElementAt(3).Name);
        }
    }
}

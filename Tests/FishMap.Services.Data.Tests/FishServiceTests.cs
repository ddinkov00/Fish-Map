namespace FishMap.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Web.ViewModels.Fish;
    using Moq;
    using Xunit;

    public class FishServiceTests
    {
        [Fact]
        public async Task CreatingFishShouldIncreaseFishRepoCount()
        {
            var fishList = new List<Fish>();
            var mockFishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            mockFishRepo.Setup(x => x.AddAsync(It.IsAny<Fish>()))
                .Callback((Fish fish) => fishList.Add(fish));
            var fishService = new FishServices(mockFishRepo.Object);

            var fishToAdd = new CreateFishInListInputModel
            {
                WeightInKilos = 4,
                LengthInCentimeters = 3,
                FishSpeciesId = 2,
            };

            var beforeAddFishCount = fishList.Count;
            var addedFishId = await fishService.CreateAsync(fishToAdd, 1);
            var afterAddFishCount = fishList.Count;

            Assert.Equal(beforeAddFishCount + 1, afterAddFishCount);
        }

        [Fact]
        public void GetAllByTripIdShouldReturnAllFishWhichHaveTripIdEqualToThePassedValue()
        {
            var fishList = new List<Fish>
            {
                new Fish
                {
                    TripId = 1,
                    FishSpecies = new FishSpecies
                    {
                        Name = "pystyrva",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdasdasd",
                        },
                        new Image
                        {
                            Url = "asdfaefsv",
                        },
                    },
                },
                new Fish
                {
                    TripId = 1,
                    FishSpecies = new FishSpecies
                    {
                        Name = "pystyrva",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdasdasd",
                        },
                        new Image
                        {
                            Url = "asdfaefsv",
                        },
                    },
                },
                new Fish
                {
                    TripId = 2,
                    FishSpecies = new FishSpecies
                    {
                        Name = "pystyrva",
                    },
                    Weight = 1,
                    Length = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "asdasdasd",
                        },
                        new Image
                        {
                            Url = "asdfaefsv",
                        },
                    },
                },
            };

            var mockFishRepo = new Mock<IDeletableEntityRepository<Fish>>();
            mockFishRepo.Setup(x => x.AllAsNoTracking()).Returns(fishList.AsQueryable());
            var fishService = new FishServices(mockFishRepo.Object);

            var fishByTripId = fishService.GetAllByTripId(1);

            Assert.Equal(2, fishByTripId.Count());
        }
    }
}

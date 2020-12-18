using FishMap.Data.Common.Repositories;
using FishMap.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FishMap.Services.Data.Tests
{
    public class TownServiceTests
    {
        [Fact]
        public void GetNearesTownIdShoudReturnTheNearestTownId()
        {
            var townList = new List<Town>()
            {
                new Town
                {
                    Name = "Plovdiv",
                    LocationLatitude = 42.1433f,
                    LocationLongtitude = 24.7489f,
                },
                new Town
                {
                    Name = "Sofiq",
                    LocationLatitude = 42.6975f,
                    LocationLongtitude = 23.3241f,
                },
                new Town
                {
                    Name = "Varna",
                    LocationLatitude = 43.2078f,
                    LocationLongtitude = 27.9169f,
                },
                new Town
                {
                    Name = "Burgas",
                    LocationLatitude = 42.5000f,
                    LocationLongtitude = 27.4667f,
                },
                new Town
                {
                    Name = "Ruse",
                    LocationLatitude = 43.8475f,
                    LocationLongtitude = 25.9544f,
                },
            };

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            townRepo.Setup(x => x.AllAsNoTracking()).Returns(townList.AsQueryable());

            var townService = new TownsService(townRepo.Object);

            var nearPlovdiv = townService.GetNearestTownName(42.1f, 24.7f);
            Assert.Equal(nearPlovdiv, townList.ElementAt(0).Name);

            var nearSofia = townService.GetNearestTownName(42.7f, 23.3f);
            Assert.Equal(nearSofia, townList.ElementAt(1).Name);

            var nearVarna = townService.GetNearestTownName(43.2f, 28f);
            Assert.Equal(nearVarna, townList.ElementAt(2).Name);

            var nearBurgas = townService.GetNearestTownName(42.5f, 27.5f);
            Assert.Equal(nearBurgas, townList.ElementAt(3).Name);

            var nearRuse = townService.GetNearestTownName(43.8f, 26f);
            Assert.Equal(nearRuse, townList.ElementAt(4).Name);
        }

        [Fact]
        public void GetNearesTownNameShoudReturnTheNearestTownName()
        {
            var townList = new List<Town>()
            {
                new Town
                {
                    Id = 1,
                    LocationLatitude = 42.1433f,
                    LocationLongtitude = 24.7489f,
                },
                new Town
                {
                    Id = 2,
                    LocationLatitude = 42.6975f,
                    LocationLongtitude = 23.3241f,
                },
                new Town
                {
                    Id = 3,
                    LocationLatitude = 43.2078f,
                    LocationLongtitude = 27.9169f,
                },
                new Town
                {
                    Id = 4,
                    LocationLatitude = 42.5000f,
                    LocationLongtitude = 27.4667f,
                },
                new Town
                {
                    Id = 5,
                    LocationLatitude = 43.8475f,
                    LocationLongtitude = 25.9544f,
                },
            };

            var townRepo = new Mock<IDeletableEntityRepository<Town>>();
            townRepo.Setup(x => x.AllAsNoTracking()).Returns(townList.AsQueryable());

            var townService = new TownsService(townRepo.Object);

            var nearPlovdiv = townService.GetNearestTownId(42.1f, 24.7f);
            Assert.Equal(nearPlovdiv, townList.ElementAt(0).Id);

            var nearSofia = townService.GetNearestTownId(42.7f, 23.3f);
            Assert.Equal(nearSofia, townList.ElementAt(1).Id);

            var nearVarna = townService.GetNearestTownId(43.2f, 28f);
            Assert.Equal(nearVarna, townList.ElementAt(2).Id);

            var nearBurgas = townService.GetNearestTownId(42.5f, 27.5f);
            Assert.Equal(nearBurgas, townList.ElementAt(3).Id);

            var nearRuse = townService.GetNearestTownId(43.8f, 26f);
            Assert.Equal(nearRuse, townList.ElementAt(4).Id);
        }
    }
}

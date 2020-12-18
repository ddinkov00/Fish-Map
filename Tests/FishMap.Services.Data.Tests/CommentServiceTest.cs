using FishMap.Data.Common.Repositories;
using FishMap.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FishMap.Services.Data.Tests
{
    public class CommentServiceTest
    {
        [Fact]
        public void GetComentsByTripIdShouldReturnTheCorrectComents()
        {
            var list = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Content = "ааа",
                    TripId = 1,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 2,
                    Content = "ббб",
                    TripId = 2,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 3,
                    Content = "ввв",
                    TripId = 1,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 4,
                    Content = "ггг",
                    TripId = 3,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(() => list.AsQueryable());
            var service = new CommentService(mockRepo.Object);

            var comments = service.GetComentsByTripId(1);

            Assert.Equal("ааа", comments.ElementAt(0).Content);
            Assert.Equal("ввв", comments.ElementAt(1).Content);
        }

        [Fact]
        public void GetComentsByTripIdShouldReturnTheCorrectNumberOfCommentsComents()
        {
            var list = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Content = "ааа",
                    TripId = 1,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 2,
                    Content = "ббб",
                    TripId = 2,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 3,
                    Content = "ввв",
                    TripId = 1,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
                new Comment
                {
                    Id = 4,
                    Content = "ггг",
                    TripId = 3,
                    UserId = "1",
                    User = new ApplicationUser
                    {
                        Email = "asd@abv.bg",
                    },
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(() => list.AsQueryable());
            var service = new CommentService(mockRepo.Object);

            var comments = service.GetComentsByTripId(1);

            Assert.Equal(2, comments.Count());
        }

        [Fact]
        public async Task CreateForTripShoudIncreaseCommentsCount()
        {
            var list = new List<Comment>();

            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(() => list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => list.Add(comment));
            var service = new CommentService(mockRepo.Object);

            await service.CreateForTrip(1, "ааа", "1");
            await service.CreateForTrip(1, "ббб", "1");

            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task CreateForGroupTripShoudIncreaseCommentsCount()
        {
            var list = new List<Comment>();

            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(() => list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => list.Add(comment));
            var service = new CommentService(mockRepo.Object);

            await service.CreateForGroupTrip(1, "ааа", "1");
            await service.CreateForGroupTrip(1, "ббб", "1");

            Assert.Equal(2, list.Count());
        }
    }
}

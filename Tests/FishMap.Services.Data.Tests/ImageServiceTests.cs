namespace FishMap.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using Moq;
    using Xunit;

    public class ImageServiceTests
    {
        [Fact]
        public async Task CreaAsyncShoudIncreaseTheCountOfTheRepoByOne()
        {
            var imageList = new List<Image>
            {
                new Image
                {
                    Id = 1,
                },
                new Image
                {
                    Id = 2,
                },
                new Image
                {
                    Id = 3,
                },
            };

            var imageRepo = new Mock<IDeletableEntityRepository<Image>>();
            imageRepo.Setup(x => x.AddAsync(It.IsAny<Image>()))
                .Callback((Image image) => imageList.Add(image));
            var imageService = new ImageServices(imageRepo.Object);

            var countBeforeAdd = imageList.Count;
            await imageService.CreateAsync("fasdasdfa", 2);
            var countAfterAdd = imageList.Count;

            Assert.Equal(countAfterAdd, countBeforeAdd + 1);
        }
    }
}

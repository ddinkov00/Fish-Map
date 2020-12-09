namespace FishMap.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;

    public class ImageServices : IImageServices
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImageServices(IDeletableEntityRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task CreateAsync(string imageUri, int fishId)
        {
            await this.imageRepository.AddAsync(new Image
            {
                Url = imageUri,
                FishId = fishId,
            });

            await this.imageRepository.SaveChangesAsync();
        }
    }
}

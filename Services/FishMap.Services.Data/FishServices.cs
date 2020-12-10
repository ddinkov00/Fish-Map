namespace FishMap.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Fish;

    public class FishServices : IFishServices
    {
        private readonly IDeletableEntityRepository<Fish> fishRepository;

        public FishServices(IDeletableEntityRepository<Fish> fishRepository)
        {
            this.fishRepository = fishRepository;
        }

        public async Task<int> CreateAsync(CreateFishInListInputModel input, int tripId)
        {
            var fishToAdd = new Fish
            {
                TripId = tripId,
                Weight = input.WeightInKilos,
                Length = input.LengthInCentimeters,
                FishSpeciesId = input.FishSpeciesId,
            };

            await this.fishRepository.AddAsync(fishToAdd);
            await this.fishRepository.SaveChangesAsync();
            return fishToAdd.Id;
        }

        public IEnumerable<FishInTripViewModel> GetAllByTripId(int tripId)
        {
            return this.fishRepository.AllAsNoTracking()
                .Where(f => f.TripId == tripId)
                .Select(f => new FishInTripViewModel
                {
                    FishSpecies = f.FishSpecies.Name,
                    Weight = f.Weight,
                    Length = f.Length,
                    ImagesUrls = f.Images
                        .Select(i => i.Url),
                });
        }
    }
}

namespace FishMap.Services.Data
{
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
    }
}

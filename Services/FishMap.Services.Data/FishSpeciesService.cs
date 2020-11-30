namespace FishMap.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.FishSpecies;

    public class FishSpeciesService : IFishSpeciesService
    {
        private readonly IDeletableEntityRepository<FishSpecies> fishSpeciesRepository;

        public FishSpeciesService(IDeletableEntityRepository<FishSpecies> fishSpeciesRepository)
        {
            this.fishSpeciesRepository = fishSpeciesRepository;
        }

        public IEnumerable<FishSpeciesSelectListModel> GetAll()
        {
            return this.fishSpeciesRepository.AllAsNoTracking()
                .Select(x => new FishSpeciesSelectListModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}

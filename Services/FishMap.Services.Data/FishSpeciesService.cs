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

        public int GetAllCount()
        {
            return this.fishSpeciesRepository
                .AllAsNoTracking()
                .Count();
        }

        public IEnumerable<FishSpeciesInListViewModel> GetAllForPaging(int page, int itemsPerPage = 12)
        {
            var fishSpecies = this.fishSpeciesRepository.AllAsNoTracking()
                .Select(x => new FishSpeciesInListViewModel
                {
                    Id = x.Id,
                    ImageUri = x.Image.Uri,
                    Name = x.Name,
                    IsCarnivore = x.IsCarnivore,
                    Descripton = x.Description,
                })
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            return fishSpecies;
        }

        public IEnumerable<FishSpeciesSelectListModel> GetAllForSelectList()
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

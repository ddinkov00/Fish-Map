namespace FishMap.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels;
    using FishMap.Web.ViewModels.FishSpecies;

    public class FishSpeciesService : IFishSpeciesService
    {
        private readonly IDeletableEntityRepository<FishSpecies> fishSpeciesRepository;
        private readonly ITripsService tripsService;

        public FishSpeciesService(
            IDeletableEntityRepository<FishSpecies> fishSpeciesRepository,
            ITripsService tripsService)
        {
            this.fishSpeciesRepository = fishSpeciesRepository;
            this.tripsService = tripsService;
        }

        public int GetAllCount()
        {
            return this.fishSpeciesRepository
                .AllAsNoTracking()
                .Count();
        }

        public IEnumerable<FishSpeciesInListViewModel> GetAllForPaging(int page, int itemsPerPage = 9)
        {
            var fishSpecies = this.fishSpeciesRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new FishSpeciesInListViewModel
                {
                    Id = x.Id,
                    ImageUri = x.Image.Url,
                    Name = x.Name,
                    IsCarnivore = x.IsCarnivore,
                    Descripton = x.Description,
                })
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

        public FishSpeciesByIdViewModel GetById(int id)
        {
            var fishSpecies = this.fishSpeciesRepository.AllAsNoTracking()
                .Where(fs => fs.Id == id)
                .Select(fs => new FishSpeciesByIdViewModel
                {
                    Name = fs.Name,
                    Description = fs.Description,
                    MinimalLegalSize = fs.MinimumLegalSize,
                    ImageUri = fs.Image.Url,
                    Trips = this.tripsService.GetAllByFishSpecies(id),
                })
                .FirstOrDefault();

            return fishSpecies;
        }
    }
}

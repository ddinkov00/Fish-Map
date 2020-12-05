namespace FishMap.Web.Controllers
{
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.FishSpecies;
    using Microsoft.AspNetCore.Mvc;

    public class FishSpeciesController : Controller
    {
        private readonly IFishSpeciesService fishSpeciesService;

        public FishSpeciesController(IFishSpeciesService fishSpeciesService)
        {
            this.fishSpeciesService = fishSpeciesService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;

            var viewModel = new FishSpeciesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.fishSpeciesService.GetAllCount(),
                FishSpecies = this.fishSpeciesService.GetAllForPaging(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.fishSpeciesService.GetById(id);

            return this.View(viewModel);
        }
    }
}
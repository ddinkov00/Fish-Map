namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;

    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.GroupTrips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class GroupTripsController : BaseController
    {
        private readonly IFishSpeciesService fishService;

        public GroupTripsController(IFishSpeciesService fishService)
        {
            this.fishService = fishService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new GroupTripCreateInputModel();
            viewModel.FishSpeciesItems = this.fishService.GetAllForSelectList();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GroupTripCreateInputModel inputModel)
        {
            return this.Redirect("/");
        }
    }
}

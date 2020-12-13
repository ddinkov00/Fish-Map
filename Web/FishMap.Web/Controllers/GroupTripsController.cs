namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.GroupTrips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class GroupTripsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGroupTripsService groupTripsService;
        private readonly IFishSpeciesService fishService;

        public GroupTripsController(
            UserManager<ApplicationUser> userManager,
            IGroupTripsService groupTripsService,
            IFishSpeciesService fishService)
        {
            this.userManager = userManager;
            this.groupTripsService = groupTripsService;
            this.fishService = fishService;
        }

        [HttpGet]
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
            if (!this.ModelState.IsValid)
            {
                var viewModel = new GroupTripCreateInputModel();
                viewModel.FishSpeciesItems = this.fishService.GetAllForSelectList();
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.groupTripsService.CreateAsync(inputModel, user.Id);

            return this.Redirect("/");
        }
    }
}

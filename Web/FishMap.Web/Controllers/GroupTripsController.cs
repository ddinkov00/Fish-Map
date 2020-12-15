namespace FishMap.Web.Controllers
{
    using System;
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
            var id = await this.groupTripsService.CreateAsync(inputModel, user.Id);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;
            var viewModel = new GroupTripsListViewModel
            {
                GroupTrips = this.groupTripsService.GetAllForPaging(id, itemsPerPage),
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.groupTripsService.GetAllCount(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult ById(EnrollRouteData routeData)
        {
            this.ViewBag.Error = routeData.ErrorMessage;
            var viewModel = this.groupTripsService.GetById(routeData.Id);
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Enroll(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.groupTripsService.EnrollUser(id, user.Id);
            }
            catch (OperationCanceledException e)
            {
                var errorMessage = e.Message;
                return this.RedirectToAction(nameof(this.ById), new EnrollRouteData { Id = id, ErrorMessage = errorMessage });
            }

            return this.RedirectToAction(nameof(this.ById), new EnrollRouteData { Id = id });
        }
    }
}

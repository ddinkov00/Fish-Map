namespace FishMap.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FishMap.Common;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.GroupTrips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
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
        public IActionResult Create()
        {
            var viewModel = new GroupTripCreateInputModel();
            viewModel.FishSpeciesItems = this.fishService.GetAllForSelectList();
            return this.View(viewModel);
        }

        [HttpPost]
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

        public IActionResult Upcoming(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;
            var viewModel = new GroupTripsListViewModel
            {
                GroupTrips = this.groupTripsService.GetUpcomingForPaging(id, itemsPerPage),
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.groupTripsService.GetUpcomingCount(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(EnrollRouteData routeData)
        {
            this.ViewBag.Error = routeData.ErrorMessage;
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.groupTripsService.GetById(routeData.Id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.IsUserAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);
            viewModel.IsUserCreator = this.groupTripsService.IsUserCreator(user.Id, routeData.Id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Enroll(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.groupTripsService.EnrollUser(id, user.Id);
            }
            catch (Exception e)
            {
                if (e.Message == GlobalConstants.NotFoundExceptionMessage)
                {
                    return this.NotFound();
                }

                var errorMessage = e.Message;
                return this.RedirectToAction(nameof(this.ById), new EnrollRouteData { Id = id, ErrorMessage = errorMessage });
            }

            return this.RedirectToAction(nameof(this.ById), new EnrollRouteData { Id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName)
                && !this.groupTripsService.IsUserCreator(user.Id, id))
            {
                return this.Unauthorized();
            }

            try
            {
                await this.groupTripsService.Delete(id);
            }
            catch (NullReferenceException)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(this.Upcoming));
        }

        public IActionResult OrderBy(string order, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;

            var viewModel = new GroupTripsListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.groupTripsService.GetUpcomingCount(),
            };

            switch (order)
            {
                case "CreatedOnDesc":
                    viewModel.GroupTrips = this.groupTripsService.OrderUpcomingByCreatedOnDesc(id, itemsPerPage);
                    break;
                case "CreatedOnAsc":
                    viewModel.GroupTrips = this.groupTripsService.OrderUpcomingByCreatedOnAsc(id, itemsPerPage);
                    break;
                case "TripDateDesc":
                    viewModel.GroupTrips = this.groupTripsService.OrderUpcomingByTripDateDesc(id, itemsPerPage);
                    break;
                case "TripDateAsc":
                    viewModel.GroupTrips = this.groupTripsService.OrderUpcomingByTripDateAsc(id, itemsPerPage);
                    break;
                default:
                    return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}

namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;

    using FishMap.Common;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TripsController : BaseController
    {
        private readonly ITripsService tripService;
        private readonly UserManager<ApplicationUser> userManager;

        public TripsController(
            ITripsService tripService,
            UserManager<ApplicationUser> userManager)
        {
            this.tripService = tripService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTripInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var tripToFishData = await this.tripService.CreateAsync(input, user.Id);

            return this.RedirectToAction(nameof(FishController.Create), nameof(FishController).Replace("Controller", string.Empty), tripToFishData);
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;

            var viewModel = new TripListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.tripService.GetAllCount(),
                Trips = this.tripService.GetAllForPaging(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var viewModel = this.tripService.GetById(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.IsUserAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);
            viewModel.IsUserCreator = this.tripService.IsUserCreator(user.Id, id);

            return this.View(viewModel);
        }

        public IActionResult OrderBy(string order, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 9;

            var viewModel = new TripListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.tripService.GetAllCount(),
            };

            switch (order)
            {
                case "CreatedOnDesc":
                    viewModel.Trips = this.tripService.OrderAllByCreatedOnDesc(id, itemsPerPage);
                    break;
                case "CreatedOnAsc":
                    viewModel.Trips = this.tripService.OrderAllByCreatedOnAsc(id, itemsPerPage);
                    break;
                case "TripDateDesc":
                    viewModel.Trips = this.tripService.OrderAllByTripDateDesc(id, itemsPerPage);
                    break;
                case "TripDateAsc":
                    viewModel.Trips = this.tripService.OrderAllByTripDateAsc(id, itemsPerPage);
                    break;
                default:
                    return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName)
                && !this.tripService.IsUserCreator(user.Id, id))
            {
                return this.Unauthorized();
            }

            try
            {
                await this.tripService.Delete(id);
            }
            catch (System.NullReferenceException)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}

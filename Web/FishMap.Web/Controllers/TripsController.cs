namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;

    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TripsController : BaseController
    {
        private const string AddFishActionName = "Create";
        private const string AddFishControllerName = "Fish";

        private readonly ITripsService tripService;
        private readonly UserManager<ApplicationUser> userManager;

        public TripsController(
            ITripsService tripService,
            UserManager<ApplicationUser> userManager)
        {
            this.tripService = tripService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTripInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var tripToFishData = await this.tripService.CreateAsync(input, user.Id);

            return this.RedirectToAction(AddFishActionName, AddFishControllerName, tripToFishData);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult ById(int id)
        {
            var viewModel = this.tripService.GetById(id);
            return this.View(viewModel);
        }
    }
}

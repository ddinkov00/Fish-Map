namespace FishMap.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Fish;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class FishController : Controller
    {
        private readonly IFishSpeciesService fishSpeciesService;

        public FishController(IFishSpeciesService fishSpeciesService)
        {
            this.fishSpeciesService = fishSpeciesService;
        }

        public IActionResult Create(AddFishRouteData routeData)
        {
            this.ViewData["FishCount"] = routeData.FishCount;
            this.ViewData["TripId"] = routeData.TripId;

            var viewModel = new CreateFishListInputModel();
            viewModel.FishSpeciesItems = this.fishSpeciesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateFishListInputModel input)
        {
            return this.Json(input);
        }
    }
}

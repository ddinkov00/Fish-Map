namespace FishMap.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using FishMap.Services;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Fish;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class FishController : Controller
    {
        private readonly IFishSpeciesService fishSpeciesService;
        private readonly IFishServices fishService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageServices imageService;
        private readonly Cloudinary cloudinary;

        public FishController(
            IFishSpeciesService fishSpeciesService,
            IFishServices fishService,
            ICloudinaryService cloudinaryService,
            IImageServices imageService,
            Cloudinary cloudinary)
        {
            this.fishSpeciesService = fishSpeciesService;
            this.fishService = fishService;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
            this.cloudinary = cloudinary;
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
        public async Task<IActionResult> Create(CreateFishListInputModel input)
        {
            var sw = new Stopwatch();
            sw.Start();
            var tripId = input.TripId;

            foreach (var fishModel in input.Fish)
            {
                var fishId = await this.fishService.CreateAsync(fishModel, tripId);
                var imagesUris = await this.cloudinaryService.UploadAsync(this.cloudinary, fishModel.Images.ToList());

                foreach (var imageUri in imagesUris)
                {
                    await this.imageService.CreateAsync(imageUri, fishId);
                }
            }

            return this.Redirect("/");
        }
    }
}

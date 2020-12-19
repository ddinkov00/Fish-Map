namespace FishMap.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using FishMap.Data;
    using FishMap.Services;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ITripsService tripsService;

        public HomeController(
            ApplicationDbContext db,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            ITripsService tripsService)
        {
            this.db = db;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
            this.tripsService = tripsService;
        }

        public IActionResult Index()
        {
            var viewModel = new AllTripsForMapViewModel
            {
                Trips = this.tripsService.GetAllForMap(),
            };

            return this.View(viewModel);
        }

        /*[HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            await this.cloudinaryService.UploadAsync(this.cloudinary, files);
            return this.Redirect("/");
        }*/

        //public IActionResult Privacy()
        //{
        //    var groupTrips = this.db.Users.Select(u => u.GroupTripsHost.Where(gt => gt.WaterPoolName == "asfdasfd"));

        //    return this.View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

namespace FishMap.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using FishMap.Data;
    using FishMap.Services;
    using FishMap.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public HomeController(
            ApplicationDbContext db,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.db = db;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            await this.cloudinaryService.UploadAsync(this.cloudinary, files);
            return this.Redirect("/");
        }

        public IActionResult Privacy()
        {
            var groupTrips = this.db.Users.Select(u => u.GroupTripsHost.Where(gt => gt.WaterPoolName == "asfdasfd"));

            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

namespace FishMap.Web.Controllers
{
    using System.Diagnostics;
    using FishMap.Data.Common.Repositories;
    using System.Linq;
    using FishMap.Data.Models;
    using FishMap.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;

        public HomeController(
            IDeletableEntityRepository<Town> townRepository,
            IDeletableEntityRepository<Location> locationRepository)
        {
            this.townRepository = townRepository;
            this.locationRepository = locationRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
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

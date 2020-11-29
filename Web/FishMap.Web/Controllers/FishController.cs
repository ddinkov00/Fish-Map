namespace FishMap.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Fish;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class FishController : Controller
    {
        public IActionResult Create(AddFishRouteData routeData)
        {
            this.ViewData["FishCount"] = routeData.FishCount;
            this.ViewData["TripId"] = routeData.TripId;
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(FishInputModel input)
        {
            return this.Redirect("/");
        }
    }
}

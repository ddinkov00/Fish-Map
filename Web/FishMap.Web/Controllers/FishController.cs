namespace FishMap.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Fish;
    using FishMap.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;

    public class FishController : Controller
    {
        public IActionResult Create(TripsToFishInputModel tripData)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(FishInputModel formInput)
        {
            return this.Redirect("/");
        }
    }
}

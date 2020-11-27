namespace FishMap.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Fish;
    using Microsoft.AspNetCore.Mvc;

    public class FishController : Controller
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(FishInputModel input)
        {
            return this.Redirect("/");
        }
    }
}

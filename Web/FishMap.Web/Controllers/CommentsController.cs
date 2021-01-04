namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;

    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentService commentService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> MakeInTrip(MakeCommentTripInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.commentService.CreateForTrip(input.TripId, input.Content, user.Id);
            return this.RedirectToAction(nameof(TripsController.ById), nameof(TripsController).Replace("Controller", string.Empty), new { id = input.TripId });
        }

        [HttpPost]
        public async Task<IActionResult> MakeInGroupTrip(MakeCommentGrouptripInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.commentService.CreateForGroupTrip(input.GroupTripId, input.Content, user.Id);
            return this.RedirectToAction(nameof(TripsController.ById), nameof(GroupTripsController).Replace("Controller", string.Empty), new { id = input.GroupTripId });
        }
    }
}
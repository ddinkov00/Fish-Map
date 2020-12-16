namespace FishMap.Web.Controllers
{
    using System.Threading.Tasks;

    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<IActionResult> Make(MakeCommentInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.commentService.Create(input.TripId, input.Content, user.Id);
            return this.RedirectToAction("ById", "Trips", new { id = input.TripId });
        }
    }
}
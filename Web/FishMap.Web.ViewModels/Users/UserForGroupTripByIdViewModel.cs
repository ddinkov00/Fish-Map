namespace FishMap.Web.ViewModels
{
    using System.Linq;

    public class UserForGroupTripByIdViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Username => this.Email.Split('@').ElementAt(0);
    }
}

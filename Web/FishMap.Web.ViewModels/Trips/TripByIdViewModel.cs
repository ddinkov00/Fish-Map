namespace FishMap.Web.ViewModels.Trips
{
    using System.Collections.Generic;
    using FishMap.Web.ViewModels.Comments;
    using FishMap.Web.ViewModels.Fish;

    public class TripByIdViewModel
    {
        public int Id { get; set; }

        public string WaterPoolName { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string FishingMethod { get; set; }

        public float LocationLatitude { get; set; }

        public float LoacationLongtitude { get; set; }

        public string UserEmail { get; set; }

        public string Username => this.UserEmail.Split('@')[0];

        public bool IsUserCreator { get; set; }

        public bool IsUserAdmin { get; set; }

        public IEnumerable<FishInTripViewModel> Fish { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

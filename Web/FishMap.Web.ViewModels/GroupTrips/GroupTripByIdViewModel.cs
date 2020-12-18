namespace FishMap.Web.ViewModels.GroupTrips
{
    using FishMap.Web.ViewModels.Comments;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupTripByIdViewModel
    {
        public int Id { get; set; }

        public string WaterPoolName { get; set; }

        public string Description { get; set; }

        public string FishingDate { get; set; }

        public float FishingLatitude { get; set; }

        public float FishingLongtitude { get; set; }

        public string MeetingDate { get; set; }

        public float MeetingLatitude { get; set; }

        public float MeetingLongtitude { get; set; }

        public string FishingMethod { get; set; }

        public string TargetFishSpecies { get; set; }

        public string HostEmail { get; set; }

        public string HostName => this.HostEmail
            .Split('@').ElementAt(0);

        public int GuestsCount { get; set; }

        public int AllSeats { get; set; }

        public bool IsUserAdmin { get; set; }

        public bool IsUserCreator { get; set; }

        public int FreeSeats => this.AllSeats - this.GuestsCount;

        public IEnumerable<UserForGroupTripByIdViewModel> Guests { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

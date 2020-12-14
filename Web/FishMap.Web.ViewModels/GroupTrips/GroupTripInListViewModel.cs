namespace FishMap.Web.ViewModels.GroupTrips
{
    public class GroupTripInListViewModel
    {
        public int Id { get; set; }

        public string WaterPoolName { get; set; }

        public string TargetFishSecies { get; set; }

        public string HostEmail { get; set; }

        public string HostUsername => this.HostEmail != null ? this.HostEmail.Split('@')[0] : string.Empty;

        public int GuestsCount { get; set; }

        public int AllSeats { get; set; }

        public string NearestCity { get; set; }

        public string TripDate { get; set; }

        public int FreeSeats => this.AllSeats - this.GuestsCount;
    }
}

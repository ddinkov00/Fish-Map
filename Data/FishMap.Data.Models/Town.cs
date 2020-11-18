namespace FishMap.Data.Models
{
    using FishMap.Data.Common.Models;

    public class Town : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}

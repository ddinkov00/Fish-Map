namespace FishMap.Web.ViewModels.Fish
{
    using System.Collections.Generic;

    public class FishInTripViewModel
    {
        public string FishSpecies { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public IEnumerable<string> ImagesUrls { get; set; }
    }
}

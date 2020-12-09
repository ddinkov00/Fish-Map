namespace FishMap.Web.ViewModels.FishSpecies
{
    using System.Collections.Generic;

    using FishMap.Web.ViewModels.Trips;

    public class FishSpeciesByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? MinimalLegalSize { get; set; }

        public string ImageUri { get; set; }

        public IEnumerable<TripByFishSpeciesViewModel> Trips { get; set; }
    }
}

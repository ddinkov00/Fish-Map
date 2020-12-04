namespace FishMap.Web.ViewModels.FishSpecies
{
    using System.Collections.Generic;

    public class FishSpeciesListViewModel : PagingViewModel
    {
        public IEnumerable<FishSpeciesInListViewModel> FishSpecies { get; set; }
    }
}

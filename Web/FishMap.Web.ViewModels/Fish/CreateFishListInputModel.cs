namespace FishMap.Web.ViewModels.Fish
{
    using System.Collections.Generic;

    using FishMap.Web.ViewModels.FishSpecies;

    public class CreateFishListInputModel
    {
        public CreateFishInListInputModel FishInputModel { get; set; }

        public IEnumerable<CreateFishInListInputModel> Fish { get; set; }

        public IEnumerable<FishSpeciesSelectListModel> FishSpeciesItems { get; set; }
    }
}

namespace FishMap.Services.Data.Contracts
{
    using System.Collections.Generic;

    using FishMap.Web.ViewModels.FishSpecies;

    public interface IFishSpeciesService
    {
        int GetAllCount();

        IEnumerable<FishSpeciesInListViewModel> GetAllForPaging(int page, int itemsPerGage = 12);

        IEnumerable<FishSpeciesSelectListModel> GetAllForSelectList();
    }
}

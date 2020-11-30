namespace FishMap.Services.Data.Contracts
{
    using System.Collections.Generic;

    using FishMap.Data.Models;
    using FishMap.Web.ViewModels.FishSpecies;

    public interface IFishSpeciesService
    {
        IEnumerable<FishSpeciesSelectListModel> GetAll();
    }
}

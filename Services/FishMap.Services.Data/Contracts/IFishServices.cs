namespace FishMap.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Fish;

    public interface IFishServices
    {
        Task<int> CreateAsync(CreateFishInListInputModel input, int tripId);
    }
}

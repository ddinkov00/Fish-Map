namespace FishMap.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImageServices
    {
        Task CreateAsync(string imageUri, int fishId);
    }
}

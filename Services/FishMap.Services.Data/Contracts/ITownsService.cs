namespace FishMap.Services.Data.Contracts
{
    public interface ITownsService
    {
        string GetNearestCity(float latitude, float longtitude);
    }
}

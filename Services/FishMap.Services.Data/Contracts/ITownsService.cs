namespace FishMap.Services.Data.Contracts
{
    public interface ITownsService
    {
        string GetNearestTownName(float latitude, float longtitude);

        int GetNearestTownId(float latitude, float longtitude);
    }
}

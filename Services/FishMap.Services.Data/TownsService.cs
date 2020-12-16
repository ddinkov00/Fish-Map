namespace FishMap.Services.Data
{
    using System;
    using System.Linq;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;

    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townsRepository;

        public TownsService(IDeletableEntityRepository<Town> townsRepository)
        {
            this.townsRepository = townsRepository;
        }

        public int GetNearestTownId(float latitude, float longtitude)
        {
            var minDistance = float.MaxValue;
            int nearestCityId = 0;

            var towns = this.townsRepository.AllAsNoTracking()
                .Select(t => new
                {
                    t.Id,
                    Latitude = t.LocationLatitude,
                    Longtitude = t.LocationLongtitude,
                }).ToList();

            foreach (var town in towns)
            {
                var distance = (float)Math.Sqrt(Math.Pow(latitude - town.Latitude, 2) + Math.Pow(longtitude - town.Longtitude, 2));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCityId = town.Id;
                }
            }

            return nearestCityId;
        }

        public string GetNearestTownName(float latitude, float longtitude)
        {
            var minDistance = float.MaxValue;
            var closestCityName = string.Empty;

            var towns = this.townsRepository.AllAsNoTracking()
                .Select(t => new
                {
                    t.Name,
                    Latitude = t.LocationLatitude,
                    Longtitude = t.LocationLongtitude,
                }).ToList();

            foreach (var town in towns)
            {
                var distance = (float)Math.Sqrt(Math.Pow(latitude - town.Latitude, 2) + Math.Pow(longtitude - town.Longtitude, 2));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCityName = town.Name;
                }
            }

            return closestCityName;
        }
    }
}

namespace FishMap.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using FishMap.Data.Models;
    using FishMap.Data.Seeding.DTOs;
    using Newtonsoft.Json;

    public class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            var townsJson = File.ReadAllText(@"..\..\Data\FishMap.Data\Seeding\Data\Towns.json");
            var deserializedTowns = JsonConvert.DeserializeObject<List<TownDto>>(townsJson);

            foreach (var town in deserializedTowns)
            {
                await dbContext.Towns.AddAsync(new Town
                {
                    Name = town.Name,
                    Location = new Location
                    {
                        Longtitude = town.Location.Longtitude,
                        Latitude = town.Location.Latitude,
                    },
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

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

    public class FishSpeciesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FishSpecies.Any())
            {
                return;
            }

            var fishSpeciesJson = File.ReadAllText(@"..\..\Data\FishMap.Data\Seeding\Data\FishSpecies.json");
            var deserializedFishSpecies = JsonConvert.DeserializeObject<List<FishSpeciesDto>>(fishSpeciesJson);

            foreach (var fishSpecies in deserializedFishSpecies)
            {
                var fishSpeciesToAdd = new FishSpecies
                {
                    Name = fishSpecies.Name,
                    Description = fishSpecies.Description,
                    IsCarnivore = fishSpecies.IsCarnivore,
                    MinimumLegalSize = fishSpecies.MinimumLegalSize,
                    Image = new Image
                    {
                        Url = fishSpecies.Image.Uri,
                    },
                };

                await dbContext.AddAsync(fishSpeciesToAdd);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

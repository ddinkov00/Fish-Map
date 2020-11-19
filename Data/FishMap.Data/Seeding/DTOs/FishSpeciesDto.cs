namespace FishMap.Data.Seeding.DTOs
{
    using FishMap.Data.Models;

    public class FishSpeciesDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCarnivore { get; set; }

        public ImageDto Image { get; set; }

        public int? MinimumLegalSize { get; set; }
    }
}

namespace FishMap.Services
{
    using System;

    public class FishingMethodService : IFishingMethodService
    {
        private const string BaitFishingBg = "Риболов на жива стръв";
        private const string FlyFishingBg = "Риболов на муха";
        private const string SpinningBg = "Спининг";
        private const string TrollingBg = "Тролинг";

        public string TranslateFishingmethod(string fishingMethodInEnglish)
        {
            var fishingMethodInBulgarian = string.Empty;

            switch (fishingMethodInEnglish)
            {
                case "BaitFishing":
                    fishingMethodInBulgarian = BaitFishingBg;
                    break;
                case "FlyFishing":
                    fishingMethodInBulgarian = FlyFishingBg;
                    break;
                case "Spinning":
                    fishingMethodInBulgarian = SpinningBg;
                    break;
                case "Trolling":
                    fishingMethodInBulgarian = TrollingBg;
                    break;
                default:
                    break;
            }

            return fishingMethodInBulgarian;
        }
    }
}

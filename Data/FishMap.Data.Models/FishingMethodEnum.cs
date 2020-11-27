namespace FishMap.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum FishingMethodEnum
    {
        [Display(Name = "Риболов на жива стръв")]
        BaitFishing = 1,

        [Display(Name = "Риболов на муха")]
        FlyFishing = 2,

        [Display(Name = "Спининг")]
        Spinning = 3,

        [Display(Name = "Тролинг")]
        Trolling = 4,
    }
}

using System.Security.Policy;

namespace SportsShop.ViewModels
{
    public class DrinksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Price { get; set; }
        public string Volume { get; set; } = default!;
        public string VolumeUnit { get; set; } = default!;
        public string Flavor { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
    }
}

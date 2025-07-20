using Entities.Models;

namespace ServiceContracts.DTO.ClothDto
{
    public class ClothResponse
    {
        public int Id { get; set; }
        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string? Code { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public static class ClothResponseExtensions
    {
        public static ClothResponse ToClothResponse( this Cloth cloth)
        {
            return new ClothResponse
            {
                Id = cloth.Product.Id,
                Size = cloth.Size,
                Color = cloth.Color,
                Material = cloth.Material,
                ImagePath = cloth.ImagePath ?? "https://via.placeholder.com/150",
                Name = cloth.Product.Name,
                Description = cloth.Product.Description,
                Producer = cloth.Product.Producer,
                Code = cloth.Product.Code,
                Price = (int)cloth.Product.Price,
                Quantity = cloth.Product.Quantity
            };
        }
    }
}

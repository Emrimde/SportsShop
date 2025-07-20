using Entities.Models;

namespace ServiceContracts.DTO.WeightPlateDto
{
    public class WeightPlateResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public int Price { get; set; }
        public string Weight { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string? ImagePath { get; set; } = default!;
    }
    public static class WeightPlateExtensions
    {
        public static WeightPlateResponse ToWeightPlateResponse(this WeightPlate weightPlate)
        {
            return new WeightPlateResponse()
            {
                ProductId = weightPlate.Product.Id,
                Name = weightPlate.Product.Name,
                Description = weightPlate.Product.Description,
                Producer = weightPlate.Product.Producer,
                Price = weightPlate.Product.Price,
                Weight = weightPlate.Weight,
                Type = weightPlate.Weight,
                ImagePath = weightPlate.ImagePath,
            };
        }
    }
}

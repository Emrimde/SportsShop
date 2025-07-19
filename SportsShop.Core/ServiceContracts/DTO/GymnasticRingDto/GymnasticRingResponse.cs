using Entities.Models;

namespace ServiceContracts.DTO.GymnasticRingDto
{
    public class GymnasticRingResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public int Price { get; set; }
        public int MaximumLoad { get; set; }
        public string Material { get; set; } = default!;
        public string TapeLength { get; set; } = default!;
        public string? ImagePath { get; set; } = default!;
    }

    public static class GymnasticRingResponseExtensions
    {
        public static GymnasticRingResponse ToGymnasticResponse(this GymnasticRing gymnasticRing)
        {
            return new GymnasticRingResponse
            {
                ProductId = gymnasticRing.ProductId,
                Name = gymnasticRing.Product.Name,
                Description = gymnasticRing.Product.Description,
                Producer = gymnasticRing.Product.Producer,
                Price = gymnasticRing.Product.Price,
                MaximumLoad = gymnasticRing.MaximumLoad,
                Material = gymnasticRing.Material,
                TapeLength = gymnasticRing.TapeLength,
                ImagePath = gymnasticRing.ImagePath,
            };
        }
    }
}

using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.ServiceContracts.DTO.TrainingRubberDto
{
    public class TrainingRubberResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public int Price { get; set; }
        public string? ImagePath { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Resistance { get; set; } = default!;
    }

    public static class TrainingRubberExtensions
    {
        public static TrainingRubberResponse ToTrainingRubberResponse(this TrainingRubber trainingRubber)
        {
            return new TrainingRubberResponse()
            {
                ProductId = trainingRubber.ProductId,
                Name = trainingRubber.Product.Name,
                Description = trainingRubber.Product.Description,
                Producer = trainingRubber.Product.Producer,
                Price = trainingRubber.Product.Price,
                ImagePath = trainingRubber.Product.ImagePath,
                Color = trainingRubber.Color,
                Resistance = trainingRubber.Resistance,
            };
        }
    }
}

using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.DrinkDto
{
    public class DrinkResponse
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

    public static class DrinkResponseExtensions
    {
        public static DrinkResponse ToDrinkResponse(this Drink drink)
        {
            return new DrinkResponse()
            {
                Id = drink.ProductId,
                Name = drink.Product.Name,
                Description = drink.Product.Description,
                Producer = drink.Product.Producer,
                Code = drink.Product.Code ?? "",
                Price = drink.Product.Price,
                Volume = drink.Volume,
                VolumeUnit = drink.VolumeUnit,
                Flavor = drink.Flavor,
                ImagePath = drink.ImagePath,
            };
        }
    }
}

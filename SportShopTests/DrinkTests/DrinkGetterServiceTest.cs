using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;
using Services;

namespace SportShopTests.DrinkTests
{
    public class DrinkGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly IDrinkGetterService _drinkGetterService;
        private readonly SportsShopDbContext _context;
        public DrinkGetterServiceTest()
        {
            _fixture = new Fixture();

            DbContextOptions<SportsShopDbContext> options = new DbContextOptionsBuilder<SportsShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new SportsShopDbContext(options);
            _drinkGetterService = new DrinkGetterService(_context);
        }

        #region GetAllDrinks

        [Fact]
        public async Task GetAllDrinks_ReturnsEmptyList()
        {
            //Act
            List<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllDrinks_ReturnsAllDrinks()
        {
            //Arrange
            List<Product> products = _fixture.Build<Product>().With(item => item.IsActive, true).CreateMany(5).ToList();
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<Drink> drinks = _fixture.Build<Drink>().Without(item => item.Product).CreateMany(5).ToList();
            _context.Drinks.AddRange(drinks);
            int index = 0;
            drinks.ForEach(item => item.ProductId = products[index++].Id);
            await _context.SaveChangesAsync();

            //Act
            List<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllDrinks_ReturnsExactlyOneDrink()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Drink drink = _fixture.Build<Drink>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();

            //Act
            List<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion
    }
}

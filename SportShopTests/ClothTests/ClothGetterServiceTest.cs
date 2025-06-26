using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;
using Services;

namespace SportShopTests.ClothTests
{
    public class ClothGetterServiceTest
    {
        private readonly IClothGetterService _clothGetterService;
        private readonly IFixture _fixture;
        private readonly SportsShopDbContext _context;

        public ClothGetterServiceTest()
        {
            _fixture = new Fixture();

            var options = new DbContextOptionsBuilder<SportsShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new SportsShopDbContext(options);
            _clothGetterService = new ClothGetterService(_context);
        }

        #region GetAllClothes

        [Fact]
        public async Task GetAllClothes_ReturnsEmptyList()
        {
            //Act
            List<ClothResponse> clothes = await _clothGetterService.GetAllClothes();

            //Assert
            clothes.Should().BeEmpty();
            clothes.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllClothes_ReturnAllClothes()
        {
            //Arrange, creating products, cloths
            List<Product> products = _fixture.Build<Product>()
                .With(p => p.IsActive, true)
                .CreateMany(5)
                .ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<Cloth> clothes = _fixture.Build<Cloth>()
           .Without(c => c.Product)
           .CreateMany(5)
           .ToList();

            int idx = 0;
            clothes.ForEach(cloth => cloth.ProductId = products[idx++].Id);

            _context.Clothes.AddRange(clothes);
            await _context.SaveChangesAsync();

            //Act
            List<ClothResponse> result = await _clothGetterService.GetAllClothes();

            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllClothes_ReturnsExactlyOneRecord()
        {
            //Arrange 
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Cloth cloth = _fixture.Build<Cloth>().Without(c => c.Product).With(item => item.ProductId, product.Id).Create();
            _context.Clothes.Add(cloth);
            await _context.SaveChangesAsync();

            //Act
            List<ClothResponse> result = await _clothGetterService.GetAllClothes();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion
    }
}

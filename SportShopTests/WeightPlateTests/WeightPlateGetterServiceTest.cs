using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IWeightPlate;


namespace SportShopTests.WeightPlateTests
{
    public class WeightPlateGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly SportsShopDbContext _context;
        private readonly IWeightPlateGetterService _weightPlateGetterService;

        public WeightPlateGetterServiceTest()
        {
            _fixture = new Fixture();
            DbContextOptions<SportsShopDbContext> options = new DbContextOptionsBuilder<SportsShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new SportsShopDbContext(options);
           
        }

        #region GetAllWeightPlates

        [Fact]
        public async Task GetAllWeightPlates_ReturnsEmptyList()
        {
            //Act
            List<WeightPlateResponse> gymnasticRings = await _weightPlateGetterService.GetAllWeightPlates();

            //Assert
            gymnasticRings.Should().BeEmpty();
            gymnasticRings.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllWeightPlates_ReturnAll()
        {
            //Arrange
            List<Product> products = _fixture.Build<Product>()
                .With(p => p.IsActive, true)
                .CreateMany(5)
                .ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<WeightPlate> weightPlates = _fixture.Build<WeightPlate>()
           .Without(c => c.Product)
           .CreateMany(5)
           .ToList();

            int idx = 0;
            weightPlates.ForEach(item => item.ProductId = products[idx++].Id);

            _context.WeightPlates.AddRange(weightPlates);
            await _context.SaveChangesAsync();

            //Act
            List<WeightPlateResponse> result = await _weightPlateGetterService.GetAllWeightPlates();

            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllWeightPlates_ReturnsExactlyOneRecord()
        {
            //Arrange 
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            WeightPlate weightPlate = _fixture.Build<WeightPlate>().Without(c => c.Product).With(item => item.ProductId, product.Id).Create();
            _context.WeightPlates.Add(weightPlate);
            await _context.SaveChangesAsync();

            //Act
            List<WeightPlateResponse> result = await _weightPlateGetterService.GetAllWeightPlates();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion

        #region GetWeightPlateById

        [Fact]
        public async Task GetWeightPlateById_ReturnProperWeightPlate()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            WeightPlate weightPlate = _fixture.Build<WeightPlate>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.WeightPlates.Add(weightPlate);
            await _context.SaveChangesAsync();

            //Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(weightPlate.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(weightPlate.ProductId);
        }

        [Fact]
        public async Task GetWeightPlateById_WeightPlateIsNull()
        {
            int missingId = 123456;

            // Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetWeightPlateById_IsActiveProperty_IsNull()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, false).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            WeightPlate weightPlate = _fixture.Build<WeightPlate>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.WeightPlates.Add(weightPlate);
            await _context.SaveChangesAsync();

            //Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(weightPlate.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

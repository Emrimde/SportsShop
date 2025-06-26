using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ICloth;
using ServiceContracts.Interfaces.ISupplement;
using Services;

namespace SportShopTests.SupplementTests
{
    public class SupplementGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly SportsShopDbContext _context;
        private readonly ISupplementGetterService _supplementGetterService;

        public SupplementGetterServiceTest()
        {
            _fixture = new Fixture();
            DbContextOptions<SportsShopDbContext> options = new DbContextOptionsBuilder<SportsShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new SportsShopDbContext(options);
            _supplementGetterService = new SupplementGetterService(_context);
        }

        #region GetSupplementById

        [Fact]
        public async Task GetSupplementById_ReturnProperSupplement()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Supplement supplement = _fixture.Build<Supplement>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.Supplements.Add(supplement);
            await _context.SaveChangesAsync();

            //Act
            SupplementResponse? result = await _supplementGetterService.GetSupplementById(supplement.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(supplement.ProductId);
        }

        [Fact]
        public async Task GetSupplementById_SupplementIsNull()
        {
            int missingId = 123456;

            // Act
            SupplementResponse? result = await _supplementGetterService.GetSupplementById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSupplementById_SupplementIsActive_IsNull()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, false).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Supplement supplement = _fixture.Build<Supplement>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.Supplements.Add(supplement);
            await _context.SaveChangesAsync();

            //Act
            SupplementResponse? result = await _supplementGetterService.GetSupplementById(supplement.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

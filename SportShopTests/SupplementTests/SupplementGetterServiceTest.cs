using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.SupplementDto;
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

        #region GetAllSupplements

        [Fact]
        public async Task GetAllSupplements_ReturnsEmptyList()
        {
            //Act
            List<SupplementResponse> supplements = await _supplementGetterService.GetAllSupplements();

            //Assert
            supplements.Should().BeEmpty();
            supplements.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllSupplements_ReturnAllSupplements()
        {
            //Arrange
            List<Product> products = _fixture.Build<Product>()
                .With(p => p.IsActive, true)
                .CreateMany(5)
                .ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<Supplement> supplements = _fixture.Build<Supplement>()
           .Without(c => c.Product)
           .CreateMany(5)
           .ToList();

            int idx = 0;
            supplements.ForEach(cloth => cloth.ProductId = products[idx++].Id);

            _context.Supplements.AddRange(supplements);
            await _context.SaveChangesAsync();

            //Act
            List<SupplementResponse> result = await _supplementGetterService.GetAllSupplements();

            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllSupplements_ReturnsExactlyOneRecord()
        {
            //Arrange 
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Supplement supplement = _fixture.Build<Supplement>().Without(c => c.Product).With(item => item.ProductId, product.Id).Create();
            _context.Supplements.Add(supplement);
            await _context.SaveChangesAsync();

            //Act
            List<SupplementResponse> result = await _supplementGetterService.GetAllSupplements();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion

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

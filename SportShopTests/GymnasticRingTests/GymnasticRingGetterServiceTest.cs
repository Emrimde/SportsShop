using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.Interfaces.IGymnasticRing;


namespace SportShopTests.GymnasticRingTests
{
    public class GymnasticRingGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly SportsShopDbContext _context;
        private readonly IGymnasticRingGetterService _gymnasticRingGetterService;

        public GymnasticRingGetterServiceTest()
        {
            _fixture = new Fixture();
            DbContextOptions<SportsShopDbContext> options = new DbContextOptionsBuilder<SportsShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new SportsShopDbContext(options);
            
        }

        #region GetAllGymnasticRings

        [Fact]
        public async Task GetAllGymnasticRings_ReturnsEmptyList()
        {
            //Act
            List<GymnasticRingResponse> gymnasticRings = await _gymnasticRingGetterService.GetAllGymnasticRings();

            //Assert
            gymnasticRings.Should().BeEmpty();
            gymnasticRings.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllGymnasticRings_ReturnAll()
        {
            //Arrange
            List<Product> products = _fixture.Build<Product>()
                .With(p => p.IsActive, true)
                .CreateMany(5)
                .ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<GymnasticRing> gymnasticRings = _fixture.Build<GymnasticRing>()
           .Without(c => c.Product)
           .CreateMany(5)
           .ToList();

            int idx = 0;
            gymnasticRings.ForEach(item => item.ProductId = products[idx++].Id);

            _context.GymnasticRings.AddRange(gymnasticRings);
            await _context.SaveChangesAsync();

            //Act
            List<GymnasticRingResponse> result = await _gymnasticRingGetterService.GetAllGymnasticRings();

            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllGymnasticRings_ReturnsExactlyOneRecord()
        {
            //Arrange 
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            GymnasticRing gymnasticRing = _fixture.Build<GymnasticRing>().Without(c => c.Product).With(item => item.ProductId, product.Id).Create();
            _context.GymnasticRings.Add(gymnasticRing);
            await _context.SaveChangesAsync();

            //Act
            List<GymnasticRingResponse> result = await _gymnasticRingGetterService.GetAllGymnasticRings();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion

        #region GetGymnasticRingById

        [Fact]
        public async Task GetGymnasticRingById_ReturnProperGymnasticRing()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            GymnasticRing gymnasticRing = _fixture.Build<GymnasticRing>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.GymnasticRings.Add(gymnasticRing);
            await _context.SaveChangesAsync();

            //Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(gymnasticRing.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(gymnasticRing.ProductId);
        }

        [Fact]
        public async Task GetGymnasticRingById_GymnasticRingIsNull()
        {
            int missingId = 123456;

            // Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetGymnasticRingById_IsActiveProperty_IsNull()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, false).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            GymnasticRing gymnasticRing = _fixture.Build<GymnasticRing>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.GymnasticRings.Add(gymnasticRing);
            await _context.SaveChangesAsync();

            //Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(gymnasticRing.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

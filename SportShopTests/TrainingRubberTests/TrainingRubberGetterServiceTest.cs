using AutoFixture;
using Entities.DatabaseContext;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.Interfaces.ITrainingRubber;
using Services.TrainingRubber;

namespace SportShopTests.TrainingRubberTests
{
    public class TrainingRubberGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly SportsShopDbContext _context;
        private readonly ITrainingRubberGetterService _trainingRubberGetterService;

        public TrainingRubberGetterServiceTest()
        {
            _fixture = new Fixture();
            DbContextOptions<SportsShopDbContext> options = new DbContextOptionsBuilder<SportsShopDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new SportsShopDbContext(options);
            _trainingRubberGetterService = new TrainingRubberGetterService(_context);
        }

        #region GetAllTrainingRubbers

        [Fact]
        public async Task GetAllTrainingRubbers_ReturnsEmptyList()
        {
            //Act
            List<TrainingRubberResponse> trainingRubbers = await _trainingRubberGetterService.GetAllTrainingRubbers();

            //Assert
            trainingRubbers.Should().BeEmpty();
            trainingRubbers.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllTrainingRubbers_ReturnAllTrainingRubbers()
        {
            //Arrange
            List<Product> products = _fixture.Build<Product>()
                .With(p => p.IsActive, true)
                .CreateMany(5)
                .ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            List<TrainingRubber> trainingRubbers = _fixture.Build<TrainingRubber>()
                .Without(c => c.Product)
                .CreateMany(5)
                .ToList();

            int idx = 0;
            trainingRubbers.ForEach(item => item.ProductId = products[idx++].Id);

            _context.TrainingRubbers.AddRange(trainingRubbers);
            await _context.SaveChangesAsync();

            //Act
            List<TrainingRubberResponse> result = await _trainingRubberGetterService.GetAllTrainingRubbers();

            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAllTrainingRubbers_ReturnsExactlyOneRecord()
        {
            //Arrange 
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TrainingRubber trainingRubber = _fixture.Build<TrainingRubber>().Without(c => c.Product).With(item => item.ProductId, product.Id).Create();
            _context.TrainingRubbers.Add(trainingRubber);
            await _context.SaveChangesAsync();

            //Act
            List<TrainingRubberResponse> result = await _trainingRubberGetterService.GetAllTrainingRubbers();

            //Assert
            result.Should().HaveCount(1);
        }

        #endregion

        #region GetTrainingRubberById

        [Fact]
        public async Task GetTrainingRubberById_ReturnProperTrainingRubber()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, true).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TrainingRubber trainingRubber = _fixture.Build<TrainingRubber>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.TrainingRubbers.Add(trainingRubber);
            await _context.SaveChangesAsync();

            //Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(trainingRubber.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(trainingRubber.ProductId);
        }

        [Fact]
        public async Task GetTrainingRubberById_TrainingRubberIsNull()
        {
            int missingId = 123456;

            // Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetTrainingRubberById_IsActiveProperty_Null()
        {
            //Arrange
            Product product = _fixture.Build<Product>().With(item => item.IsActive, false).Create();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TrainingRubber trainingRubber = _fixture.Build<TrainingRubber>().Without(item => item.Product).With(item => item.ProductId, product.Id).Create();
            _context.TrainingRubbers.Add(trainingRubber);
            await _context.SaveChangesAsync();

            //Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(trainingRubber.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

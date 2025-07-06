using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.Interfaces.ITrainingRubber;
using Services;

namespace SportShopTests.TrainingRubberTests
{
    public class TrainingRubberGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<ITrainingRubberRepository> _trainingRubberRepositoryMock;
        private readonly ITrainingRubberRepository _trainingRubberRepository;
        private readonly ITrainingRubberGetterService _trainingRubberGetterService;

        public TrainingRubberGetterServiceTest()
        {
            _trainingRubberRepositoryMock = new Mock<ITrainingRubberRepository>();
            _trainingRubberRepository = _trainingRubberRepositoryMock.Object;
            _trainingRubberGetterService = new TrainingRubberGetterService(_trainingRubberRepository);
            _fixture = new Fixture();
        }

        #region GetAllTrainingRubbers

        [Fact]
        public void GetAllTrainingRubbers_ReturnsEmptyList()
        {
            //Arrange
            _trainingRubberRepositoryMock.Setup(item => item.GetAllTrainingRubbers()).Returns(new List<TrainingRubber>().AsQueryable());

            //Act
            List<TrainingRubberResponse> trainingRubbers =  _trainingRubberGetterService.GetAllTrainingRubbers();

            //Assert
            trainingRubbers.Should().BeEmpty();
        }

        [Fact]
        public void GetAllTrainingRubbers_ReturnAllTrainingRubbers()
        {
            //Arrange
            List<TrainingRubber> trainingRubbers = new List<TrainingRubber>()
            {
                _fixture.Create<TrainingRubber>(),
                _fixture.Create<TrainingRubber>(),
                _fixture.Create<TrainingRubber>()
            };

            List<TrainingRubberResponse> expected = trainingRubbers.Select(item => item.ToTrainingRubberResponse()).ToList();

            _trainingRubberRepositoryMock.Setup(item => item.GetAllTrainingRubbers()).Returns(trainingRubbers.AsQueryable());

            //Act
            List<TrainingRubberResponse> result =  _trainingRubberGetterService.GetAllTrainingRubbers();

            result.Should().HaveCount(3);
            expected.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllTrainingRubbers_ReturnsExactlyOneRecord()
        {
            //Arrange 
            TrainingRubber trainingRubber = _fixture.Create<TrainingRubber>();
            TrainingRubberResponse expected = trainingRubber.ToTrainingRubberResponse();
            _trainingRubberRepositoryMock.Setup(item => item.GetAllTrainingRubbers()).Returns(new List<TrainingRubber>(){ trainingRubber}.AsQueryable());

            //Act
            List<TrainingRubberResponse> result =  _trainingRubberGetterService.GetAllTrainingRubbers();

            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetTrainingRubberById

        [Fact]
        public async Task GetTrainingRubberById_ReturnProperTrainingRubber()
        {
            //Arrange
            TrainingRubber trainingRubber = _fixture.Create<TrainingRubber>();
            TrainingRubberResponse expected = trainingRubber.ToTrainingRubberResponse();

            _trainingRubberRepositoryMock.Setup(item => item.GetTrainingRubberById(trainingRubber.ProductId)).ReturnsAsync(trainingRubber);

            //Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(trainingRubber.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetTrainingRubberById_TrainingRubberIsNull()
        {
            int missingId = 123456;
            _trainingRubberRepositoryMock.Setup(item => item.GetTrainingRubberById(missingId)).ReturnsAsync(null as TrainingRubber);

            // Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetTrainingRubberById_IsActiveProperty_Null()
        {
            //Arrange


            TrainingRubber trainingRubber = _fixture.Create<TrainingRubber>();
            trainingRubber.Product.IsActive = false;

            _trainingRubberRepositoryMock.Setup(item => item.GetTrainingRubberById(trainingRubber.ProductId)).ReturnsAsync(null as TrainingRubber);

            //Act
            TrainingRubberResponse? result = await _trainingRubberGetterService.GetTrainingRubberById(trainingRubber.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

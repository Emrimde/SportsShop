using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IWeightPlate;
using Services;

namespace SportShopTests.WeightPlateTests
{
    public class WeightPlateGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly IWeightPlateGetterService _weightPlateGetterService;
        private readonly IWeightPlateRepository _weightPlateRepository;
        private readonly Mock<IWeightPlateRepository> _weightPlateRepositoryMock;

        public WeightPlateGetterServiceTest()
        {
            _fixture = new Fixture();
            _weightPlateRepositoryMock = new Mock<IWeightPlateRepository>();
            _weightPlateRepository = _weightPlateRepositoryMock.Object;
            _weightPlateGetterService = new WeightPlateGetterService(_weightPlateRepository);
        }

        #region GetAllWeightPlates

        [Fact]
        public void GetAllWeightPlates_ReturnsEmptyList()
        {
            //Arrange
            _weightPlateRepositoryMock.Setup(item => item.GetAllWeightPlates()).Returns(new List<WeightPlate>().AsQueryable());

            //Act
            List<WeightPlateResponse> weightPlates =  _weightPlateGetterService.GetAllWeightPlates();

            //Assert
            weightPlates.Should().BeEmpty();
        }

        [Fact]
        public void GetAllWeightPlates_ReturnAll()
        {
            //Arrange
            List<WeightPlate> weightPlates = new List<WeightPlate>()
            {
                _fixture.Create<WeightPlate>(),
                _fixture.Create<WeightPlate>(),
                _fixture.Create<WeightPlate>()
            };

            _weightPlateRepositoryMock.Setup(item => item.GetAllWeightPlates()).Returns(weightPlates.AsQueryable());

            List<WeightPlateResponse> expected = weightPlates.Select(item => item.ToWeightPlateResponse()).ToList();

            //Act
            List<WeightPlateResponse> result =  _weightPlateGetterService.GetAllWeightPlates();

            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllWeightPlates_ReturnsExactlyOneRecord()
        {
            //Arrange 
            WeightPlate weightPlate = _fixture.Create<WeightPlate>();
            WeightPlateResponse expected = weightPlate.ToWeightPlateResponse();
            _weightPlateRepositoryMock.Setup(item => item.GetAllWeightPlates()).Returns(new List<WeightPlate>(){weightPlate}.AsQueryable());

            //Act
            List<WeightPlateResponse> result =  _weightPlateGetterService.GetAllWeightPlates();

            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetWeightPlateById

        [Fact]
        public async Task GetWeightPlateById_ReturnProperWeightPlate()
        {
            //Arrange
            WeightPlate weightPlate = _fixture.Create<WeightPlate>();
            WeightPlateResponse expected = weightPlate.ToWeightPlateResponse();

            _weightPlateRepositoryMock.Setup(item => item.GetWeightPlateById(weightPlate.ProductId)).ReturnsAsync(weightPlate);

            //Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(weightPlate.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetWeightPlateById_WeightPlateIsNull()
        {
            int missingId = 123456;
            _weightPlateRepositoryMock.Setup(item => item.GetWeightPlateById(missingId)).ReturnsAsync(null as WeightPlate);

            // Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetWeightPlateById_IsActiveProperty_IsNull()
        {
            //Arrange
            WeightPlate weightPlate = _fixture.Create<WeightPlate>();

            weightPlate.Product.IsActive = false;

            _weightPlateRepositoryMock.Setup(item => item.GetWeightPlateById(weightPlate.ProductId)).ReturnsAsync(null as WeightPlate);

            //Act
            WeightPlateResponse? result = await _weightPlateGetterService.GetWeightPlateById(weightPlate.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.Interfaces.IGymnasticRing;
using Services;

namespace SportShopTests.GymnasticRingTests
{
    public class GymnasticRingGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IGymnasticRingRepository> _gymnasticRingRepositoryMock;
        private readonly IGymnasticRingRepository _gymnasticRingRepository;
        private readonly IGymnasticRingGetterService _gymnasticRingGetterService;

        public GymnasticRingGetterServiceTest()
        {
            _fixture = new Fixture();
            _gymnasticRingRepositoryMock = new Mock<IGymnasticRingRepository>();
            _gymnasticRingRepository = _gymnasticRingRepositoryMock.Object;
            _gymnasticRingGetterService = new GymnasticRingGetterService(_gymnasticRingRepository);
        }

        #region GetAllGymnasticRings

        [Fact]
        public void GetAllGymnasticRings_ReturnsEmptyList()
        {
            //Arrange
            _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).Returns(new List<GymnasticRing>().AsQueryable());

            //Act
            List<GymnasticRingResponse> gymnasticRings = _gymnasticRingGetterService.GetAllGymnasticRings();

            //Assert
            gymnasticRings.Should().BeEmpty();
        }

        [Fact]
        public void GetAllGymnasticRings_ReturnAll()
        {
            //Arrange
            List<GymnasticRing> gymnasticRings = new List<GymnasticRing>()
           {
               _fixture.Create<GymnasticRing>(),
               _fixture.Create<GymnasticRing>(),
               _fixture.Create<GymnasticRing>()
           };

            List<GymnasticRingResponse> expected = gymnasticRings.Select(item => item.ToGymnasticResponse()).ToList();
            _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).Returns(gymnasticRings.AsQueryable());

            //Act
            List<GymnasticRingResponse> result = _gymnasticRingGetterService.GetAllGymnasticRings();

            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllGymnasticRings_ReturnsExactlyOneRecord()
        {
            //Arrange 
            GymnasticRing gymnasticRing = _fixture.Create<GymnasticRing>();
            GymnasticRingResponse expected = gymnasticRing.ToGymnasticResponse();

            _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).Returns(new List<GymnasticRing>() {gymnasticRing}.AsQueryable());

            //Act
            List<GymnasticRingResponse> result = _gymnasticRingGetterService.GetAllGymnasticRings();

            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetGymnasticRingById

        [Fact]
        public async Task GetGymnasticRingById_ReturnProperGymnasticRing()
        {
            //Arrange
            GymnasticRing gymnasticRing = _fixture.Create<GymnasticRing>();
            GymnasticRingResponse expected = gymnasticRing.ToGymnasticResponse();

            _gymnasticRingRepositoryMock.Setup(item => item.GetGymnasticRingById(gymnasticRing.ProductId)).ReturnsAsync(gymnasticRing);

            //Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(gymnasticRing.ProductId);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetGymnasticRingById_GymnasticRingIsNull()
        {
            int missingId = 123456;
            _gymnasticRingRepositoryMock.Setup(item => item.GetGymnasticRingById(missingId)).ReturnsAsync(null as GymnasticRing);

            // Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetGymnasticRingById_IsActiveProperty_IsNull()
        {
            //Arrange
            GymnasticRing gymnasticRing = _fixture.Create<GymnasticRing>();
            gymnasticRing.Product.IsActive = false;
            _gymnasticRingRepositoryMock.Setup(item => item.GetGymnasticRingById(gymnasticRing.ProductId)).ReturnsAsync(null as GymnasticRing);

            //Act
            GymnasticRingResponse? result = await _gymnasticRingGetterService.GetGymnasticRingById(gymnasticRing.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

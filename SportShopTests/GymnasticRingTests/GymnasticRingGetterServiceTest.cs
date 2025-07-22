using AutoFixture;
using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.GymnasticRingDto;
using SportsShop.Core.ServiceContracts.Interfaces.IGymnasticRing;
using SportsShop.Core.Services.GymnasticRingServices;

namespace SportShopTests.GymnasticRingTests;
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
    public async Task GetAllGymnasticRings_ReturnsEmptyList()
    {
        //Arrange
        _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).ReturnsAsync(new List<GymnasticRing>());

        //Act
        IReadOnlyList<GymnasticRingResponse> gymnasticRings = await _gymnasticRingGetterService.GetAllGymnasticRings();

        //Assert
        gymnasticRings.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllGymnasticRings_ReturnAll()
    {
        //Arrange
        IEnumerable<GymnasticRing> gymnasticRings = new List<GymnasticRing>()
       {
           _fixture.Create<GymnasticRing>(),
           _fixture.Create<GymnasticRing>(),
           _fixture.Create<GymnasticRing>()
       };

        List<GymnasticRingResponse> expected = gymnasticRings.Select(item => item.ToGymnasticResponse()).ToList();
        _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).ReturnsAsync(gymnasticRings);

        //Act
        IReadOnlyList<GymnasticRingResponse> result = await _gymnasticRingGetterService.GetAllGymnasticRings();

        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetAllGymnasticRings_ReturnsExactlyOneRecord()
    {
        //Arrange 
        GymnasticRing gymnasticRing = _fixture.Create<GymnasticRing>();
        GymnasticRingResponse expected = gymnasticRing.ToGymnasticResponse();

        _gymnasticRingRepositoryMock.Setup(item => item.GetAllGymnasticRings()).ReturnsAsync(new List<GymnasticRing>() {gymnasticRing});

        //Act
        IReadOnlyList<GymnasticRingResponse> result = await _gymnasticRingGetterService.GetAllGymnasticRings();

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

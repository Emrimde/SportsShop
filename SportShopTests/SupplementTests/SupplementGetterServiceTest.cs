using AutoFixture;
using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.SupplementDto;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplement;
using SportsShop.Core.Services.SupplementServices;

namespace SportShopTests.SupplementTests;
public class SupplementGetterServiceTest
{
    private readonly IFixture _fixture;
    private readonly ISupplementGetterService _supplementGetterService;
    private readonly ISupplementRepository _supplementRepository;
    private readonly Mock<ISupplementRepository> _supplementRepositoryMock;
    public SupplementGetterServiceTest()
    {
        _fixture = new Fixture();
        _supplementRepositoryMock = new Mock<ISupplementRepository>();
        _supplementRepository = _supplementRepositoryMock.Object;
        _supplementGetterService = new SupplementGetterService(_supplementRepository);
    }

    #region GetAllSupplements

    [Fact]
    public void GetAllSupplements_ReturnsEmptyList()
    {
        //Arrange
        _supplementRepositoryMock.Setup(item => item.GetAllSupplements()).Returns(new List<Supplement>().AsQueryable());

        List<SupplementResponse> supplements = _supplementGetterService.GetAllSupplements();
        //Assert
        supplements.Should().BeEmpty();
    }

    [Fact]
    public void GetAllSupplements_ReturnAllSupplements()
    {
        //Arrange
        List<Supplement> supplements = new List<Supplement>()
        {
            _fixture.Create<Supplement>(),
            _fixture.Create<Supplement>(),
            _fixture.Create<Supplement>()
        };
        List<SupplementResponse> expected = supplements.Select(item => item.ToSupplementResponse()).ToList();
        _supplementRepositoryMock.Setup(item => item.GetAllSupplements()).Returns(supplements.AsQueryable());

        //Act
        List<SupplementResponse> result = _supplementGetterService.GetAllSupplements();

        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetAllSupplements_ReturnsExactlyOneRecord()
    {
        //Arrange 
        Supplement supplement = _fixture.Create<Supplement>();
        SupplementResponse expected = supplement.ToSupplementResponse();
        _supplementRepositoryMock.Setup(item => item.GetAllSupplements()).Returns(new List<Supplement>(){supplement}.AsQueryable());

        //Act
        List<SupplementResponse> result = _supplementGetterService.GetAllSupplements();

        //Assert
        result.Should().HaveCount(1);
        result.Single().Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GetSupplementById

    [Fact]
    public async Task GetSupplementById_ReturnProperSupplement()
    {
        //Arrange
        Supplement supplement = _fixture.Create<Supplement>();
        SupplementResponse expected = supplement.ToSupplementResponse();
        _supplementRepositoryMock.Setup(item => item.GetSupplementById(supplement.ProductId)).ReturnsAsync(supplement);

        //Act
        SupplementResponse? result = await _supplementGetterService.GetSupplementById(supplement.ProductId);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetSupplementById_SupplementIsNull()
    {
        int missingId = 123456;
        _supplementRepositoryMock.Setup(item => item.GetSupplementById(missingId)).ReturnsAsync(null as Supplement);
        // Act
        SupplementResponse? result = await _supplementGetterService.GetSupplementById(missingId);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetSupplementById_SupplementIsActive_IsNull()
    {
        //Arrange
        Supplement supplement = _fixture.Create<Supplement>();
        supplement.Product.IsActive = false;

        //Act
        SupplementResponse? result = await _supplementGetterService.GetSupplementById(supplement.ProductId);

        //Assert
        result.Should().BeNull();
    }
    #endregion
}

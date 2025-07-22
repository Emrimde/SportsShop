using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.Services.AddressServices;

namespace SportShopTests.AddressTests;
public class AddressDeleterServiceTest
{
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressDeleterService _addressDeleterService;

    public AddressDeleterServiceTest()
    {
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _addressRepository = _addressRepositoryMock.Object;
        _addressDeleterService = new AddressDeleterService(_addressRepository);
    }

    #region DeleteAddress

    [Fact]
    public async Task DeleteAddress_AddressFound_ShouldReturnTrue()
    {
        _addressRepositoryMock.Setup(item => item.DeleteAddress(It.IsAny<Address>())).ReturnsAsync(true);

        //Act
        bool result = await _addressDeleterService.DeleteAddress(10, Guid.NewGuid());

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAddress_AddressNotFound_ShouldReturnFalse()
    {
        int unknownAddressId = 9999;
        _addressRepositoryMock.Setup(item => item.DeleteAddress(It.IsAny<Address>())).ReturnsAsync(false);
        bool result = await _addressDeleterService.DeleteAddress(unknownAddressId, Guid.NewGuid());

        result.Should().BeFalse();
    }

    #endregion
}

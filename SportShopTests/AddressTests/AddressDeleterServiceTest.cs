using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.Interfaces.IAddress;
using Services;

namespace SportShopTests.AddressTests
{
    public class AddressDeleterServiceTest
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressDeleterService _addressDeleterService;
        private readonly IFixture _fixture;

        public AddressDeleterServiceTest()
        {
            _fixture = new Fixture();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _addressRepository = _addressRepositoryMock.Object;
            _addressDeleterService = new AddressDeleterService(_addressRepository);
        }

        #region DeleteAddress

        [Fact]
        public async Task DeleteAddress_AddressFound_ShouldReturnTrue()
        {
            int id = 10;
            Guid guid = Guid.NewGuid();
            _addressRepositoryMock.Setup(item => item.DeleteAddress(It.IsAny<Address>())).ReturnsAsync(true);

            //Act
            bool result = await _addressDeleterService.DeleteAddress(id, guid);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAddress_AddressNotFound_ShouldReturnFalse()
        {
            int unknownAddressId = 9999;
            Guid guid = Guid.NewGuid();
            _addressRepositoryMock.Setup(item => item.DeleteAddress(It.IsAny<Address>())).ReturnsAsync(false);
            bool result = await _addressDeleterService.DeleteAddress(unknownAddressId, guid);

            result.Should().BeFalse();
        }

        #endregion
    }
}

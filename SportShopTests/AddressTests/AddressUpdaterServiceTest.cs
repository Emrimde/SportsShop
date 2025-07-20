using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;
using Services;

namespace SportShopTests.AddressTests
{
    public class AddressUpdaterServiceTest
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressUpdaterService _addressUpdaterService;
        private readonly IFixture _fixture;

        public AddressUpdaterServiceTest()
        {
            _fixture = new Fixture();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _addressRepository = _addressRepositoryMock.Object;
            _addressUpdaterService = new AddressUpdaterService(_addressRepository);
        }

        [Fact]
        public async Task UpdateAddress_ValidModel_ShouldReturnUpdatedResponse()
        {
            // Arrange
            AddressUpdateRequest model = _fixture.Build<AddressUpdateRequest>().Create();
            Address address = model.ToAddress();
            AddressResponse expectedResponse = address.ToAddressResponse();

            _addressRepositoryMock
                .Setup(item => item.UpdateAddress(It.IsAny<Address>()))
                .ReturnsAsync(address);

            // Act
            AddressResponse result = await _addressUpdaterService.UpdateAddress(model);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }
    }
}

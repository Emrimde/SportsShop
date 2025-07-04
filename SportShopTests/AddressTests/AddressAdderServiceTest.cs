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
    public class AddressAdderServiceTest
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressAdderService _addressAdderService;
        private readonly IFixture _fixture;

        public AddressAdderServiceTest()
        {
            _fixture = new Fixture();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _addressRepository = _addressRepositoryMock.Object;
            _addressAdderService = new AddressAdderService(_addressRepository);
        }

        #region AddAddress 

        [Fact]
        public async Task AddAddress_ShoulReturnAddress()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            AddressAddRequest addressAddRequest = _fixture.Build<AddressAddRequest>().Create();
            Address address = addressAddRequest.ToAddress(userId);
            AddressResponse expected = address.ToAddressResponse();

            _addressRepositoryMock.Setup(item => item.AddAddress(It.IsAny<Address>())).ReturnsAsync(address);

            //Act
            AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, userId);
            expected.Id = result!.Id;

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AddAddress_AddressIsNull_ShouldReturnNull()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            AddressAddRequest addressAddRequest = null!;
 
            //Act
            AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, userId);
            
            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}


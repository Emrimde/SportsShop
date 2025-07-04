using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;
using Services.IAddress;

namespace SportShopTests.AddressTests
{
    public class AddressGetterServiceTest
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressGetterService _addressGetterService;
        private readonly IFixture _fixture;

        public AddressGetterServiceTest()
        {
            _fixture = new Fixture();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _addressRepository = _addressRepositoryMock.Object;
            _addressGetterService = new AddressGetterService(_addressRepository);
        }

        #region GetAllAddresses

        [Fact]
        public void Should_ReturnEmptyList_When_UserHasNoAddresses()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            _addressRepositoryMock.Setup(item => item.GetAllAddresses(userId)).Returns(new List<Address>().AsQueryable());

            //Act
            List<AddressResponse> result = _addressGetterService.GetAllAddresses(Guid.NewGuid());

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllAddresses_ReturnAllUserAddresses()
        {
            //Arrange
            List<Address> addresses = new List<Address>()
            {
                _fixture.Build<Address>().With(item => item.User, null as User).Create(),
                _fixture.Build<Address>().With(item => item.User, null as User).Create(),
                _fixture.Build<Address>().With(item => item.User, null as User).Create()
            };
            Guid userId = Guid.NewGuid();

            List<AddressResponse> expected = addresses.Select(item => item.ToAddressResponse()).ToList();

            _addressRepositoryMock.Setup(item => item.GetAllAddresses(userId)).Returns(addresses.AsQueryable());

            List<AddressResponse> result = _addressGetterService.GetAllAddresses(userId);

            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllAddresses_ReturnsOneAddressForUser()
        {
            //Arrange
            Address address = _fixture.Build<Address>().With(item => item.User, null as User).Create();
            AddressResponse expected = address.ToAddressResponse();
            Guid userId = Guid.NewGuid();

            _addressRepositoryMock.Setup(item => item.GetAllAddresses(userId)).Returns(new List<Address>{ address }.AsQueryable());

            //Act
            List<AddressResponse> result = _addressGetterService.GetAllAddresses(userId);

            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetAddressById

        [Fact]
        public async Task GetAddressById_ReturnCorrectAddress()
        {
            //Arrange
            Address address = _fixture.Build<Address>().With(item => item.User, null as User).Create();
            AddressResponse expected = address.ToAddressResponse();

            _addressRepositoryMock.Setup(item => item.GetAddressById(address.Id)).ReturnsAsync(address);

            //Act
            AddressResponse? result = await _addressGetterService.GetAddressById(address.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAddressById_AddressIsNotFound_ToBeNull()
        {
            //Arrange
            int missingId = 123456;

            _addressRepositoryMock.Setup(item => item.GetAddressById(missingId)).ReturnsAsync(null as Address);
            // Act
            AddressResponse? result = await _addressGetterService.GetAddressById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAddressById_IsActiveEqualToFalse_ToBeNull()
        {
            //Arrange
            Address address = _fixture.Build<Address>().With(item => item.User, null as User).Create();
            address.IsActive = false;

            _addressRepositoryMock.Setup(item => item.GetAddressById(address.Id)).ReturnsAsync(null as Address);

            //Act
            AddressResponse? result = await _addressGetterService.GetAddressById(address.Id);

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region IsAddressProvided

        [Fact]
        public void IsAddressProvided_AllPropertiesNotNull_ShouldBeTrue()
        {
            //Arrange
            AddressAddRequest request = _fixture.Create<AddressAddRequest>();

            //Act
            bool result = _addressGetterService.IsAddressProvided(request);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsAddressProvided_StreetIsNull_ShouldBeFalse()
        {
            //Arrange
            AddressAddRequest request = _fixture.Build<AddressAddRequest>().With(item => item.Street, null as string).Create();

            //Act
            bool result = _addressGetterService.IsAddressProvided(request);

            //Assert
            result.Should().BeTrue();
        }

        #endregion
    }
}

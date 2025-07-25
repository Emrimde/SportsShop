﻿using AutoFixture;
using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.Services.AddressService;

namespace SportShopTests.AddressTests;
public class AddressAdderServiceTest
{
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly Mock<IAddressValidationService> _addressValidationServiceMock;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressAdderService _addressAdderService;
    private readonly IAddressValidationService _addressValidationService;
    private readonly IFixture _fixture;

    public AddressAdderServiceTest()
    {
        _fixture = new Fixture();
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _addressValidationServiceMock = new Mock<IAddressValidationService>();
        _addressRepository = _addressRepositoryMock.Object;
        _addressValidationService = _addressValidationServiceMock.Object;
        _addressAdderService = new AddressAdderService(_addressRepository, _addressValidationService);
    }

    #region AddAddress 

    [Fact]
    public async Task AddAddress_ShoulReturnAddress()
    {
        //Arrange
        Guid userId = Guid.NewGuid();
        AddressAddRequest addressAddRequest = _fixture.Build<AddressAddRequest>().Create();
        Address address = addressAddRequest.ToAddress(userId);
        address.Country = new Country
        {
            Id = address.CountryId, // albo Guid.NewGuid()
            Name = "Test Country"
        };
        AddressResponse expected = address.ToAddressResponse();

        _addressRepositoryMock.Setup(item => item.AddAddress(It.IsAny<Address>())).ReturnsAsync(address);

        //Act
        AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, userId);
        expected.Id = result!.Id;

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task AddAddress_AddressIsNull_ShouldThrowArgumentNullException()
    {
        //Arrange
        Guid userId = Guid.NewGuid();
        AddressAddRequest addressAddRequest = null!;

        Func <Task> action = async () => await _addressAdderService.AddAddress(addressAddRequest, userId);

        //Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddAddress_UserIdIsEmpty_ShouldThrowArgumentNullException()
    {
        //Arrange
        Guid emptyUserId = Guid.Empty;
        AddressAddRequest addressAddRequest = _fixture.Create<AddressAddRequest>();

        Func<Task> action = async () => await _addressAdderService.AddAddress(addressAddRequest, emptyUserId );

        //Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }
    #endregion
}


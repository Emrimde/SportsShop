using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.SupplierDto;
using ServiceContracts.Interfaces.ISupplier;
using Services;

namespace SportShopTests.SupplierTest
{
    public class SupplierGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly ISupplierRepository _supplierRepository;
        private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
        private readonly ISupplierGetterService _supplierGetterService;

        public SupplierGetterServiceTest()
        {
            _fixture = new Fixture();
            _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _supplierRepository = _supplierRepositoryMock.Object;
            _supplierGetterService = new SupplierGetterService(_supplierRepository);
        }

        #region GetAllSuppliers

        [Fact]
        public void GetAllSuppliers_ShouldReturnEmptyList()
        {
            //Arrange
            _supplierRepositoryMock.Setup(item => item.GetAllSuppliers())
                .Returns(new List<Supplier>().AsQueryable());

            //Act
            List<SupplierResponse> result = _supplierGetterService.GetAllSuppliers();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllSuppliers_ShouldReturnAllSuppliers()
        {
            //Arrange
            List<Supplier> suppliers = new List<Supplier>()
            {
                _fixture.Create<Supplier>(),
                _fixture.Create<Supplier>(),
                _fixture.Create<Supplier>(),
            };

            List<SupplierResponse> expected = suppliers.Select(item => item.ToSupplierResponse()).ToList();

            _supplierRepositoryMock.Setup(item => item.GetAllSuppliers())
                .Returns(suppliers.AsQueryable());

            //Act
            List<SupplierResponse> result = _supplierGetterService.GetAllSuppliers();

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        #endregion
    }
}

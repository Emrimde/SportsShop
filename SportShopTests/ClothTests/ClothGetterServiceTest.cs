using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;
using Services;

namespace SportShopTests.ClothTests
{
    public class ClothGetterServiceTest
    {
        private readonly IClothGetterService _clothGetterService;
        private readonly IFixture _fixture;
        private readonly IClothRepository _clothRepository;
        private readonly Mock<IClothRepository> _clothRepositoryMock;

        public ClothGetterServiceTest()
        {
            _fixture = new Fixture();
            _clothRepositoryMock = new Mock<IClothRepository>();
            _clothRepository = _clothRepositoryMock.Object;
            _clothGetterService = new ClothGetterService(_clothRepository);         
        }

        #region GetAllClothes

        [Fact]
        public void GetAllClothes_ReturnsEmptyList_ToBeSuccessfull()
        {
            //Arrange
            _clothRepositoryMock.Setup(item => item.GetAllClothes()).Returns(new List<Cloth>().AsQueryable());

            //Act
            List<ClothResponse> clothes =  _clothGetterService.GetAllClothes();

            //Assert
            clothes.Should().BeEmpty();
            clothes.Should().NotBeNull();
        }

        [Fact]
        public void GetAllClothes_ReturnAllClothes_ToBeSuccessfull()
        {
            //Arrange
            List<Cloth> clothes = new List<Cloth>()
            {
                _fixture.Build<Cloth>().Create(),
                _fixture.Build<Cloth>().Create(),
                _fixture.Build<Cloth>().Create()
            };

            List<ClothResponse> expected = clothes.Select(item => item.ToClothResponse()).ToList();

            _clothRepositoryMock.Setup(item => item.GetAllClothes()).Returns(clothes.AsQueryable());

            //Act
            List<ClothResponse> result =  _clothGetterService.GetAllClothes();

            //Assert
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllClothes_ReturnsExactlyOneRecord_ToBeSuccessfull()
        {
            //Arrange
            Cloth cloth = _fixture.Build<Cloth>().Create();
            ClothResponse expected = cloth.ToClothResponse();

            _clothRepositoryMock.Setup(item => item.GetAllClothes()).Returns(new List<Cloth> { cloth }.AsQueryable());

            //Act
            List<ClothResponse> result = _clothGetterService.GetAllClothes();
            
            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetClothById

        [Fact]
        public async Task GetClothById_FullClothDetails_ToBeSuccessfull()
        {
            //Arrange
            Cloth cloth = _fixture.Build<Cloth>().Create();
            ClothResponse expected = cloth.ToClothResponse();

            _clothRepositoryMock.Setup(item => item.GetClothById(cloth.ProductId)).ReturnsAsync(cloth);

            //Act
            ClothResponse? result = await _clothGetterService.GetClothById(cloth.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetClothById_ClothIsNull_ToBeNull()
        {
            //Arrange
            int missingId = 123456; 

            _clothRepositoryMock.Setup(item => item.GetClothById(missingId)).ReturnsAsync((Cloth?)null);
            // Act
            ClothResponse? result = await _clothGetterService.GetClothById(missingId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetClothById_IsActiveEqualToFalse_ToBeNull()
        {
            //Arrange
            Cloth cloth = _fixture.Build<Cloth>().Create();
            cloth.Product.IsActive = false;

            _clothRepositoryMock.Setup(item => item.GetClothById(cloth.ProductId)).ReturnsAsync(null as Cloth);

            //Act
            ClothResponse? result = await _clothGetterService.GetClothById(cloth.ProductId);

            //Assert
            result.Should().BeNull();
        }
        #endregion
    }
}

using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.DrinkDto;
using ServiceContracts.Interfaces.IDrink;
using Services;

namespace SportShopTests.DrinkTests
{
    public class DrinkGetterServiceTest
    {
        private readonly IFixture _fixture;
        private readonly IDrinkGetterService _drinkGetterService;
        private readonly IDrinkRepository _drinkRepository;
        private readonly Mock<IDrinkRepository> _drinkRepositoryMock;
        public DrinkGetterServiceTest()
        {
            _fixture = new Fixture();
            _drinkRepositoryMock = new Mock<IDrinkRepository>();
            _drinkRepository = _drinkRepositoryMock.Object;
            _drinkGetterService = new DrinkGetterService(_drinkRepository);
        }

        #region GetAllDrinks

        [Fact]
        public async void GetAllDrinks_ReturnsEmptyList_ToBeSuccessfull()
        {
            //Arrange
             _drinkRepositoryMock.Setup(item => item.GetAllDrinks()).ReturnsAsync(new List<Drink>());

            //Act
            IEnumerable<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async void GetAllDrinks_ShouldReturnAllDrinks()
        {
            //Arrange
            IEnumerable<Drink> drinks = new List<Drink>() {
                _fixture.Create<Drink>(),
                _fixture.Create<Drink>(),
                _fixture.Create<Drink>(),
            };

            List<DrinkResponse> expected = drinks.Select(item => item.ToDrinkResponse()).ToList();

            _drinkRepositoryMock.Setup(item => item.GetAllDrinks()).ReturnsAsync(drinks);

            //Act
            IEnumerable<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void GetAllDrinks_ReturnsExactlyOneDrink()
        {
            //Arrange
            Drink drink = _fixture.Create<Drink>();
            DrinkResponse expected = drink.ToDrinkResponse();

            _drinkRepositoryMock.Setup(item => item.GetAllDrinks()).ReturnsAsync(new List<Drink>(){drink});

            //Act
            IEnumerable<DrinkResponse> result = await _drinkGetterService.GetAllDrinks();

            //Assert
            result.Should().HaveCount(1);
            result.Single().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetDrinkById

        [Fact]
        public async Task GetDrinkById_ReturnProperDrink()
        {
            //Arrange
            Drink drink = _fixture.Create<Drink>();
            DrinkResponse expected = drink.ToDrinkResponse();

            _drinkRepositoryMock.Setup(item => item.GetDrinkById(drink.ProductId)).ReturnsAsync(drink);

            //Act
            DrinkResponse result = await _drinkGetterService.GetDrinkById(drink.ProductId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetDrinkById_ReturnNull()
        {
            int fakeId = 123456;
            _drinkRepositoryMock.Setup(item => item.GetDrinkById(fakeId)).ReturnsAsync(null as  Drink);

            //Act
            DrinkResponse result = await _drinkGetterService.GetDrinkById(fakeId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetDrinkById_PropertyIsActiveFalse_ReturnNull()
        {
            //Arrange
          
            Drink drink = _fixture.Create<Drink>();
            drink.Product.IsActive = false;
            
            _drinkRepositoryMock.Setup(item => item.GetDrinkById(drink.ProductId)).ReturnsAsync(null as Drink);

            //Act
            DrinkResponse result = await _drinkGetterService.GetDrinkById(drink.ProductId);

            //Assert
            result.Should().BeNull();
        }

        #endregion  
    }
}

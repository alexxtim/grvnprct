using System;
using System.Collections.Generic;
using GrosvenorPracticum_DAL;
using GrosvenorPracticum_DAL.Models;
using Moq;
using NUnit.Framework;

namespace GrosvenorPracticum_BL.Tests
{
    public class WhenFoodServiceManagerIsUsed
    {
        private IFoodServiceManager _foodServiceManager;
        private Mock<IFoodServiceRepository> _mockFoodServiceRepository;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mockFoodServiceRepository = new Mock<IFoodServiceRepository>();
            _foodServiceManager = new FoodServiceManager(_mockFoodServiceRepository.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputIsPartiallyValid()
        {
            //Arrange
            const string dayTime = "morning";
            var dishList = new[] {"1", "3", "2", "3", "2", "6"};
            var inputDayTimeAndDishes = string.Format("{0},{1}", dayTime, String.Join(", ", dishList));
            const string expectedResult = "eggs, toast, coffee(x2), error";

            var mockDishDto1 = new Mock<IDishDto>();
            mockDishDto1.SetupGet(x => x.DishName).Returns("eggs");
            mockDishDto1.SetupGet(x => x.DishTypeId).Returns("1");
            mockDishDto1.SetupGet(x => x.IsMultiple).Returns(false);
            mockDishDto1.Setup(x => x.GetFullDishName(1)).Returns("eggs");
            var mockDishDto2 = new Mock<IDishDto>();
            mockDishDto2.SetupGet(x => x.DishName).Returns("toast");
            mockDishDto2.SetupGet(x => x.DishTypeId).Returns("2");
            mockDishDto2.SetupGet(x => x.IsMultiple).Returns(false);
            mockDishDto2.Setup(x => x.GetFullDishName(1)).Returns("toast");
            var mockDishDto3 = new Mock<IDishDto>();
            mockDishDto3.SetupGet(x => x.DishName).Returns("coffee");
            mockDishDto3.SetupGet(x => x.DishTypeId).Returns("3");
            mockDishDto3.SetupGet(x => x.IsMultiple).Returns(true);
            mockDishDto3.Setup(x => x.GetFullDishName(2)).Returns("coffee(x2)");

            _mockFoodServiceRepository
                .Setup(x => x.GetDishes(dayTime, dishList))
                .Returns(new[] {mockDishDto1.Object, mockDishDto2.Object, mockDishDto3.Object});

            //Act
            var result = _foodServiceManager.GetDishes(inputDayTimeAndDishes);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
            _mockFoodServiceRepository.Verify(x => x.GetDishes(dayTime, dishList), Times.Once);
        }

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenDatTimeIsIncorrect()
        {
            //Arrange
            const string dayTime = "moooorning";
            var dishList = new[] { "1", "2" };
            var inputDayTimeAndDishes = string.Format("{0},{1}", dayTime, String.Join(", ", dishList));
            var dishDtoList = new List<IDishDto>();
            const string expectedResult = "error";

            _mockFoodServiceRepository
                .Setup(x => x.GetDishes(dayTime, dishList))
                .Returns(dishDtoList);

            //Act
            var result = _foodServiceManager.GetDishes(inputDayTimeAndDishes);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
            _mockFoodServiceRepository.Verify(x => x.GetDishes(dayTime, dishList), Times.Once);
        }

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputIsEmpty()
        {
            //Arrange
            var inputDayTimeAndDishes = string.Empty;
            const string expectedResult = "error";

            //Act
            var result = _foodServiceManager.GetDishes(inputDayTimeAndDishes);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
            _mockFoodServiceRepository.Verify(x => x.GetDishes(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        #endregion
    }
}

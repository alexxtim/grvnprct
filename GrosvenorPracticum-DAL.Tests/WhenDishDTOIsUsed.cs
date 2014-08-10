using GrosvenorPracticum_DAL.Models;
using NUnit.Framework;

namespace GrosvenorPracticum_DAL.Tests
{
    public class WhenDishDtoIsUsed
    {
        private IDishDto _dishDto;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _dishDto = new DishDto();
        }

        #endregion

        #region Tests

        [Test]
        public void ThatGetFullDishNameReturnsCorrectValueWhenMultipleDishes()
        {
            //Arrange
            _dishDto.DishName = "coffee";
            _dishDto.DishTypeId = "3";
            _dishDto.IsMultiple = true;
            const string expectedResult = "coffee(x2)";

            //Act
            var result = _dishDto.GetFullDishName(2);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ThatGetFullDishNameReturnsCorrectValueWhenSingleDish()
        {
            //Arrange
            _dishDto.DishName = "coffee";
            _dishDto.DishTypeId = "3";
            _dishDto.IsMultiple = false;
            const string expectedResult = "coffee";

            //Act
            var result = _dishDto.GetFullDishName(1);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ThatGetFullDishNameReturnsCorrectValueWhenMultipleDishIsFalse()
        {
            //Arrange
            _dishDto.DishName = "coffee";
            _dishDto.DishTypeId = "3";
            _dishDto.IsMultiple = false;
            const string expectedResult = "coffee";

            //Act
            var result = _dishDto.GetFullDishName(3);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #endregion
    }
}
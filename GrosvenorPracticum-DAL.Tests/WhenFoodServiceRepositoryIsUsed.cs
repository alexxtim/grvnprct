using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GrosvenorPracticum_DAL.Models;
using Moq;
using NUnit.Framework;

namespace GrosvenorPracticum_DAL.Tests
{
    public class WhenFoodServiceRepositoryIsUsed
    {
        private IFoodServiceRepository _foodServiceRepository;
        private Mock<IFoodServiceModelContainer> _mockFoodServiceModelContainer;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mockFoodServiceModelContainer = new Mock<IFoodServiceModelContainer>();
            _foodServiceRepository = new FoodServiceRepository(_mockFoodServiceModelContainer.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputIsValid()
        {
            //Arrange
            const string dayTime = "morning";
            var dishes = new[] {"1", "2", "3"};
            var expectedResult = new List<IDishDto>
            {
                new DishDto {DishName = "eggs", DishTypeId = "1", IsMultiple = false},
                new DishDto {DishName = "toast", DishTypeId = "2", IsMultiple = false},
                new DishDto {DishName = "coffee", DishTypeId = "3", IsMultiple = true}
            };
            InitializeFoodServiceModelContainer();
            
            //Act
            var result = _foodServiceRepository.GetDishes(dayTime, dishes).ToList();

            //Assert
            Assert.That(result.Select(x => x.DishName), Is.EqualTo(expectedResult.Select(x => x.DishName)));
            Assert.That(result.Select(x => x.DishTypeId), Is.EqualTo(expectedResult.Select(x => x.DishTypeId)));
            Assert.That(result.Select(x => x.IsMultiple), Is.EqualTo(expectedResult.Select(x => x.IsMultiple)));
        }

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputDishesIsPartiallyInvalid()
        {
            //Arrange
            const string dayTime = "morning";
            var dishes = new[] { "1", "2", "3", "6", "8" };
            var expectedResult = new List<IDishDto>
            {
                new DishDto {DishName = "eggs", DishTypeId = "1", IsMultiple = false},
                new DishDto {DishName = "toast", DishTypeId = "2", IsMultiple = false},
                new DishDto {DishName = "coffee", DishTypeId = "3", IsMultiple = true}
            };
            InitializeFoodServiceModelContainer();
            
            //Act
            var result = _foodServiceRepository.GetDishes(dayTime, dishes).ToList();

            //Assert
            Assert.That(result.Select(x => x.DishName), Is.EqualTo(expectedResult.Select(x => x.DishName)));
            Assert.That(result.Select(x => x.DishTypeId), Is.EqualTo(expectedResult.Select(x => x.DishTypeId)));
            Assert.That(result.Select(x => x.IsMultiple), Is.EqualTo(expectedResult.Select(x => x.IsMultiple)));
        }

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputDishesIsFullyInvalid()
        {
            //Arrange
            const string dayTime = "morning";
            var dishes = new[] { "6", "8" };
            InitializeFoodServiceModelContainer();

            //Act
            var result = _foodServiceRepository.GetDishes(dayTime, dishes);

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThatGetDishesReturnsCorrectValuesWhenInputDayTimeIsInvalid()
        {
            //Arrange
            const string dayTime = "moooorning";
            var dishes = new[] { "1", "2", "3" };
            InitializeFoodServiceModelContainer();
            
            //Act
            var result = _foodServiceRepository.GetDishes(dayTime, dishes);

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        #endregion

        #region Private Methods

        private void InitializeFoodServiceModelContainer()
        {
            var dishEntityList = new List<DishEntity> 
            { 
                new DishEntity { Id = 1, Name = "eggs" },
                new DishEntity { Id = 2, Name = "toast" },
                new DishEntity { Id = 3, Name = "coffee" },
                new DishEntity { Id = 4, Name = "steak" },
                new DishEntity { Id = 5, Name = "potato" },
                new DishEntity { Id = 6, Name = "wine" },
                new DishEntity { Id = 7, Name = "cake" }
            }.AsQueryable();

            var mockDishEntityDbSet = new Mock<IDbSet<DishEntity>>();
            mockDishEntityDbSet.Setup(m => m.Provider).Returns(dishEntityList.Provider);
            mockDishEntityDbSet.Setup(m => m.Expression).Returns(dishEntityList.Expression);
            mockDishEntityDbSet.Setup(m => m.ElementType).Returns(dishEntityList.ElementType);
            mockDishEntityDbSet.Setup(m => m.GetEnumerator()).Returns(dishEntityList.GetEnumerator());
            _mockFoodServiceModelContainer.SetupGet(x => x.DishEntities).Returns(mockDishEntityDbSet.Object);

            var dishTypeEntityList = new List<DishTypeEntity> 
            { 
                new DishTypeEntity {Id = 1, TypeId = "1", TypeName = "entree"},
                new DishTypeEntity {Id = 2, TypeId = "2", TypeName = "side"},
                new DishTypeEntity {Id = 3, TypeId = "3", TypeName = "drink"},
                new DishTypeEntity {Id = 5, TypeId = "4", TypeName = "dessert"},
            }.AsQueryable();

            var mockDishTypeEntityDbSet = new Mock<IDbSet<DishTypeEntity>>();
            mockDishTypeEntityDbSet.Setup(m => m.Provider).Returns(dishTypeEntityList.Provider);
            mockDishTypeEntityDbSet.Setup(m => m.Expression).Returns(dishTypeEntityList.Expression);
            mockDishTypeEntityDbSet.Setup(m => m.ElementType).Returns(dishTypeEntityList.ElementType);
            mockDishTypeEntityDbSet.Setup(m => m.GetEnumerator()).Returns(dishTypeEntityList.GetEnumerator());
            _mockFoodServiceModelContainer.SetupGet(x => x.DishTypeEntities).Returns(mockDishTypeEntityDbSet.Object);

            var dayTimeEntityList = new List<DayTimeEntity> 
            { 
                new DayTimeEntity {Id = 1, TimeName = "morning"},
                new DayTimeEntity {Id = 2, TimeName = "night"}
            }.AsQueryable();

            var mockDayTimeEntityDbSet = new Mock<IDbSet<DayTimeEntity>>();
            mockDayTimeEntityDbSet.Setup(m => m.Provider).Returns(dayTimeEntityList.Provider);
            mockDayTimeEntityDbSet.Setup(m => m.Expression).Returns(dayTimeEntityList.Expression);
            mockDayTimeEntityDbSet.Setup(m => m.ElementType).Returns(dayTimeEntityList.ElementType);
            mockDayTimeEntityDbSet.Setup(m => m.GetEnumerator()).Returns(dayTimeEntityList.GetEnumerator());
            _mockFoodServiceModelContainer.SetupGet(x => x.DayTimeEntities).Returns(mockDayTimeEntityDbSet.Object);

            var dayTimeDishEntityList = new List<DayTimeDishEntity> 
            { 
                new DayTimeDishEntity {Id = 1, DayTime = dayTimeEntityList.First(x => x.Id == 1), Dish = dishEntityList.First(x => x.Id == 1), IsMultiple = false},
                new DayTimeDishEntity {Id = 2, DayTime = dayTimeEntityList.First(x => x.Id == 1), Dish = dishEntityList.First(x => x.Id == 2), IsMultiple = false},
                new DayTimeDishEntity {Id = 3, DayTime = dayTimeEntityList.First(x => x.Id == 1), Dish = dishEntityList.First(x => x.Id == 3), IsMultiple = true},
                new DayTimeDishEntity {Id = 4, DayTime = dayTimeEntityList.First(x => x.Id == 2), Dish = dishEntityList.First(x => x.Id == 4), IsMultiple = false},
                new DayTimeDishEntity {Id = 5, DayTime = dayTimeEntityList.First(x => x.Id == 2), Dish = dishEntityList.First(x => x.Id == 5), IsMultiple = true},
                new DayTimeDishEntity {Id = 6, DayTime = dayTimeEntityList.First(x => x.Id == 2), Dish = dishEntityList.First(x => x.Id == 6), IsMultiple = false},
                new DayTimeDishEntity {Id = 7, DayTime = dayTimeEntityList.First(x => x.Id == 2), Dish = dishEntityList.First(x => x.Id == 7), IsMultiple = false}
            }.AsQueryable();

            var mockDayTimeDishEntityDbSet = new Mock<IDbSet<DayTimeDishEntity>>();
            mockDayTimeDishEntityDbSet.Setup(m => m.Provider).Returns(dayTimeDishEntityList.Provider);
            mockDayTimeDishEntityDbSet.Setup(m => m.Expression).Returns(dayTimeDishEntityList.Expression);
            mockDayTimeDishEntityDbSet.Setup(m => m.ElementType).Returns(dayTimeDishEntityList.ElementType);
            mockDayTimeDishEntityDbSet.Setup(m => m.GetEnumerator()).Returns(dayTimeDishEntityList.GetEnumerator());
            _mockFoodServiceModelContainer.SetupGet(x => x.DayTimeDishEntities).Returns(mockDayTimeDishEntityDbSet.Object);

            var dishTypeDishEntityList = new List<DishTypeDishEntity> 
            { 
                new DishTypeDishEntity {Id = 1, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 1), DishEntity = dishEntityList.First(x => x.Id == 1)},
                new DishTypeDishEntity {Id = 2, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 2), DishEntity = dishEntityList.First(x => x.Id == 2)},
                new DishTypeDishEntity {Id = 3, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 3), DishEntity = dishEntityList.First(x => x.Id == 3)},
                new DishTypeDishEntity {Id = 4, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 1), DishEntity = dishEntityList.First(x => x.Id == 4)},
                new DishTypeDishEntity {Id = 5, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 2), DishEntity = dishEntityList.First(x => x.Id == 5)},
                new DishTypeDishEntity {Id = 6, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 3), DishEntity = dishEntityList.First(x => x.Id == 6)},
                new DishTypeDishEntity {Id = 7, DishTypeEntity = dishTypeEntityList.First(x => x.Id == 5), DishEntity = dishEntityList.First(x => x.Id == 7)}
            }.AsQueryable();

            var mockDishTypeDishEntityDbSet = new Mock<IDbSet<DishTypeDishEntity>>();
            mockDishTypeDishEntityDbSet.Setup(m => m.Provider).Returns(dishTypeDishEntityList.Provider);
            mockDishTypeDishEntityDbSet.Setup(m => m.Expression).Returns(dishTypeDishEntityList.Expression);
            mockDishTypeDishEntityDbSet.Setup(m => m.ElementType).Returns(dishTypeDishEntityList.ElementType);
            mockDishTypeDishEntityDbSet.Setup(m => m.GetEnumerator()).Returns(dishTypeDishEntityList.GetEnumerator());
            _mockFoodServiceModelContainer.SetupGet(x => x.DishTypeDishEntities).Returns(mockDishTypeDishEntityDbSet.Object);
        }

        #endregion
    }
}

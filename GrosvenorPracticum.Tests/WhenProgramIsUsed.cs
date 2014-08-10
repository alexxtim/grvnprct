using Autofac;
using GrosvenorPracticum_BL;
using Moq;
using NUnit.Framework;

namespace GrosvenorPracticum.Tests
{
    public class WhenProgramIsUsed
    {
        private Mock<IConsole> _mockConsole;
        private Mock<IFoodServiceManager> _mockFoodServiceManager;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _mockConsole = new Mock<IConsole>();
            _mockFoodServiceManager = new Mock<IFoodServiceManager>();
            Program.Container = GetMockedContainer(_mockConsole.Object, _mockFoodServiceManager.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void ThatMainMethodIsCorrect()
        {
            const string inputString = "inputString";
            const string outputString = "outputString";

            //Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns(inputString);
            _mockFoodServiceManager.Setup(x => x.GetDishes(inputString)).Returns(outputString);

            //Act
            Program.Main(new string[0]);
            
            //Assert
            _mockConsole.Verify(x => x.WriteLine(outputString), Times.Once);
            _mockConsole.Verify(x => x.ReadLine(), Times.Exactly(2));
        }

        #endregion

        #region Private Methods

        private IContainer GetMockedContainer(IConsole console, IFoodServiceManager foodServiceManager)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(console).As<IConsole>();
            builder.RegisterInstance(foodServiceManager).As<IFoodServiceManager>();
            return builder.Build();
        }

        #endregion
    }
}
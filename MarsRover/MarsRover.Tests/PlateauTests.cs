using MarsRover.Core.CommandCenter;
using MarsRover.Core.Mars;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover.Tests
{
    [TestClass]
    public class PlateauTests
    {
        [TestMethod]
        public void PlateauTests_PlateauIsCreatedWithTheCorrectSize()
        {
            // Arrange
            var plateau = new Plateau();

            var commandCenter = new CommandCenter();

            var createPlateauCommand = new CreatePlateauCommand(plateau, "5 5");

            const int expectedWidth = 5;

            const int expectedHeight = 5;

            // Act
            commandCenter.SendCommand(createPlateauCommand);

            var actualWidth = plateau.Width();

            var actualHeight = plateau.Height();

            // Assert
            Assert.AreEqual(expectedWidth, actualWidth);

            Assert.AreEqual(expectedHeight, actualHeight);
        }

        [TestMethod]
        public void PlateauTests_PlateauIsCreatedWithTheCorrectSizeWhenRectangle()
        {
            // Arrange
            var plateau = new Plateau();

            var commandCenter = new CommandCenter();

            var createPlateauCommand = new CreatePlateauCommand(plateau, "3 9");

            const int expectedWidth = 3;

            const int expectedHeight = 9;

            // Act
            commandCenter.SendCommand(createPlateauCommand);

            var actualWidth = plateau.Width();

            var actualHeight = plateau.Height();

            // Assert
            Assert.AreEqual(expectedWidth, actualWidth);

            Assert.AreEqual(expectedHeight, actualHeight);
        }
    }
}

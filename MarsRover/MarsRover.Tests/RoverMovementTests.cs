using System;
using MarsRover.Core.CommandCenter;
using MarsRover.Core.Mars;
using MarsRover.Core.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover.Tests
{
    [TestClass]
    public class RoverMovementTests
    {
        private CommandCenter _commandCenter;

        private IPlateau _plateau;

        [TestInitialize]
        public void TestInit()
        {
            _commandCenter = new CommandCenter();

            _plateau = new Plateau();

            _plateau.Define(5, 5);
        }

        [TestMethod]
        public void RoverMovementTests_RoverReportsCorrectLocationAfterMovementWithRightTurns()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "3 3 E");

            _commandCenter.SendCommand(createRoverCommand);

            const string expectedLocation = "5 1 E";

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "MMRMMRMRRM");

            _commandCenter.SendCommand(moveRoverCommand);

            var actualLocation = roverSquadManager.ListRovers()[0].Location();

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);
        }

        [TestMethod]
        public void RoverMovementTests_RoverReportsCorrectLocationAfterMovementWithLeftTurns()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "1 2 N");

            _commandCenter.SendCommand(createRoverCommand);

            const string expectedLocation = "1 3 N";

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "LMLMLMLMM");

            _commandCenter.SendCommand(moveRoverCommand);

            var actualLocation = roverSquadManager.ListRovers()[0].Location();

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RoverMovementTests_ThrowsAnExceptionWhenAnInvalidMovementIsPassedInTheCommand()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "1 2 N");

            _commandCenter.SendCommand(createRoverCommand);

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "LMG");

            _commandCenter.SendCommand(moveRoverCommand);

            // Assert - Expects Exception
        }

        [TestMethod]
        public void RoverMovementTests_RoverReportsCorrectLocationWhenMovingFromEdgeToEdge()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "0 0 N");

            _commandCenter.SendCommand(createRoverCommand);

            const string expectedLocation = "5 5 E";

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "MMMMMRMMMMM");

            _commandCenter.SendCommand(moveRoverCommand);

            var actualLocation = roverSquadManager.ListRovers()[0].Location();

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);
        }

        [TestMethod]
        public void RoverMovementTests_RoverDoesNotMoveOutOfBoundsIfTheCommendWouldMoveTheRoverOutOfBounds()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "0 0 N");

            _commandCenter.SendCommand(createRoverCommand);

            const string expectedOutOfBoundsLocation = "0 10 N";

            const string expectedActualLocation = "0 5 N";

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "MMMMMMMMMM");

            _commandCenter.SendCommand(moveRoverCommand);

            var actualLocation = roverSquadManager.ListRovers()[0].Location();

            // Assert
            Assert.AreNotEqual(expectedOutOfBoundsLocation, actualLocation);

            Assert.AreEqual(expectedActualLocation, actualLocation);
        }

        [TestMethod]
        public void RoverMovementTests_RoverMovementWithRectanglePlateau()
        {
            // Arrange
            var rectangle = new Plateau();

            rectangle.Define(3, 9);

            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "2 1 N");

            _commandCenter.SendCommand(createRoverCommand);

            const string expectedLocation = "4 2 E";

            // Act
            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "MLMRRMMM");

            _commandCenter.SendCommand(moveRoverCommand);

            var actualLocation = roverSquadManager.ListRovers()[0].Location();

            // Assert
            Assert.AreEqual(expectedLocation, actualLocation);
        }

        [TestMethod]
        public void RoverMovementTests_RoverReportsCorrectLocationOfMultipleRovers()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverOneCommand = new CreateRoverCommand(roverSquadManager, "1 2 N");

            var createRoverOneMoveCommand = new MoveRoverCommand(roverSquadManager, "LMLMLMLMM");

            var createRoverTwoCommand = new CreateRoverCommand(roverSquadManager, "3 3 E");

            var createRoverTwoMoveCommand = new MoveRoverCommand(roverSquadManager, "MMRMMRMRRM");

            const string expectedLocationRoverOne = "1 3 N";

            const string expectedLocationRoverTwo = "5 1 E";

            // Act
            _commandCenter.SendCommand(createRoverOneCommand);

            _commandCenter.SendCommand(createRoverOneMoveCommand);

            _commandCenter.SendCommand(createRoverTwoCommand);

            _commandCenter.SendCommand(createRoverTwoMoveCommand);

            var actualLocationRoverOne = roverSquadManager.ListRovers()[0].Location();

            var actualLocationRoverTwo = roverSquadManager.ListRovers()[1].Location();

            // Assert
            Assert.AreEqual(expectedLocationRoverOne, actualLocationRoverOne);

            Assert.AreEqual(expectedLocationRoverTwo, actualLocationRoverTwo);
        }
    }
}
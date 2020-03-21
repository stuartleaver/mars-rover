using System;
using System.Collections.Generic;
using MarsRover.Core.CommandCenter;
using MarsRover.Core.Enums;
using MarsRover.Core.Mars;
using MarsRover.Core.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover.Tests
{
    [TestClass]
    public class RoverDeploymentTests
    {
        private IPlateau _plateau;

        private CommandCenter _commandCenter;

        [TestInitialize]
        public void TestInit()
        {
            _plateau = new Plateau();

            _plateau.Define(5, 5);

            _commandCenter = new CommandCenter();
        }

        [TestMethod]
        public void RoverDeploymentTests_CreatesRoverCorrectly()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "1 2 N");

            const int expectedRoverCount = 1;

            var expectedRovers = new List<Rover>
            {
                new Rover(_plateau, 1, 2, Direction.N)
            };

            // Act
            _commandCenter.SendCommand(createRoverCommand);

            var actualRoverCount = roverSquadManager.CountOfRovers();

            var actualRovers = roverSquadManager.ListRovers();

            // Arrange
            Assert.AreEqual(expectedRoverCount, actualRoverCount);

            CollectionAssert.AreEqual(expectedRovers, actualRovers);
        }

        [TestMethod]
        public void RoverDeploymentTests_CreatesMultipleRoversCorrectly()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverOneCommand = new CreateRoverCommand(roverSquadManager, "3 4 W");
            var createRoverTwoCommand = new CreateRoverCommand(roverSquadManager, "2 2 E");

            const int expectedRoverCount = 2;

            var expectedRovers = new List<Rover>
            {
                new Rover(_plateau, 3, 4, Direction.W),
                new Rover(_plateau, 2, 2, Direction.E)
            };

            // Act
            _commandCenter.SendCommand(createRoverOneCommand);
            _commandCenter.SendCommand(createRoverTwoCommand);

            var actualRoverCount = roverSquadManager.CountOfRovers();

            var actualRovers = roverSquadManager.ListRovers();

            // Arrange
            Assert.AreEqual(expectedRoverCount, actualRoverCount);

            CollectionAssert.AreEqual(expectedRovers, actualRovers);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RoverDeploymentTests_ThrowsAnExceptionWhenCreatingARoverOutOfBounds()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "9 9 N");

            // Act
            _commandCenter.SendCommand(createRoverCommand);

            // Assert - Expects Exception
        }

        [TestMethod]
        public void RoverDeploymentTests_ReturnsCorrectActiveRover()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverOneCommand = new CreateRoverCommand(roverSquadManager, "3 4 W");
            var createRoverTwoCommand = new CreateRoverCommand(roverSquadManager, "2 2 E");

            var expectedActiveRover = new Rover(_plateau, 2, 2, Direction.E);

            // Act
            _commandCenter.SendCommand(createRoverOneCommand);
            _commandCenter.SendCommand(createRoverTwoCommand);

            var actualActiveRover = roverSquadManager.ActiveRover();

            // Arrange
            Assert.AreEqual(expectedActiveRover, actualActiveRover);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RoverDeploymentTests_ThrowsAnExceptionWhenAnInvalidDirectionIsPassedInTheCommand()
        {
            // Arrange
            var roverSquadManager = new RoverSquadManager(_plateau);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "3 3 R");

            _commandCenter.SendCommand(createRoverCommand);

            // Act
            _commandCenter.SendCommand(createRoverCommand);

            // Assert - Expects Exception
        }
    }
}
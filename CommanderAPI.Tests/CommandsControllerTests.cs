﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AutoMapper;
using CommanderAPI.Controllers;
using CommanderAPI.Models;
using CommanderAPI.Data;
using CommanderAPI.Profiles;
using CommanderAPI.Dtos;

namespace CommanderAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        [Fact]
        public void GetCommandItems_Returns200OK_WhenDBIsEmpty()
        {
            // Arrange
            mockRepo.Setup(repo =>
                repo.GetAllCommands()).Returns(GetCommands(0));

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act 
            ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange
            mockRepo.Setup(repo =>
                repo.GetAllCommands()).Returns(GetCommands(1));

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

            // Assert
            var okResult = result.Result as OkObjectResult;

            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo =>
                repo.GetAllCommands()).Returns(GetCommands(1));

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo =>
                repo.GetAllCommands()).Returns(GetCommands(1));

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo =>
                repo.GetCommandById(0)).Returns(() => null);

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<CommandReadDto> result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_ReturnsCorrectType_WhenValidIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo =>
                repo.GetCommandById(1)).Returns(
                    new Command
                    {
                        Id = 1,
                        HowTo = "mock",
                        Platform = "Mock",
                        CommandLine = "Mock"
                    });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<CommandReadDto> result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void GetCommandByID_Returns200OK_WhenValidIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo =>
                repo.GetCommandById(1)).Returns(
                    new Command
                    {
                        Id = 1,
                        HowTo = "mock",
                        Platform = "Mock",
                        CommandLine = "Mock"
                    });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            ActionResult<CommandReadDto> result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(
                new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult<CommandReadDto> result = controller.CreateCommand(new CommandCreateDto { });

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(
                new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult<CommandReadDto> result = controller.CreateCommand(new CommandCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(
                new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult result = controller.UpdateCommand(1, new CommandUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult result = controller.UpdateCommand(1, new CommandUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult result = controller.PartialCommandUpdate(0,
                new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(
                new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult result = controller.DeleteCommand(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            ActionResult result = controller.DeleteCommand(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        private List<Command> GetCommands(int num)
        {
            List<Command> commands = new List<Command>();

            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }
    }
}

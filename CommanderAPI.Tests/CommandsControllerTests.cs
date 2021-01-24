using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AutoMapper;
using CommanderAPI.Controllers;
using CommanderAPI.Models;
using CommanderAPI.Data;
using CommanderAPI.Profiles;

namespace CommanderAPI.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommandItems_Returns200OK_WhenDBIsEmpty()
        {
            // Arrange
            var mockRepo = new Mock<ICommandAPIRepo>();

            mockRepo.Setup(repo =>
                repo.GetAllCommands()).Returns(GetCommands(0));

            var realProfile = new CommandsProfile();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);

            CommandsController controller = new CommandsController(mockRepo.Object, mapper);
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

﻿using System;
using Xunit;
using CommanderAPI.Models;

namespace CommanderAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "Some platform",
                CommandLine = "Some commandLine"
            };
        }

        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act
            testCommand.HowTo = "Execute Unit Tests";

            // Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            // Arrange

            // Act
            testCommand.Platform = "xUnit";

            // Assert
            Assert.Equal("xUnit", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            // Arrange

            // Act
            testCommand.CommandLine = "dotnet test";

            // Assert
            Assert.Equal("dotnet test", testCommand.CommandLine);
        }
    }
}

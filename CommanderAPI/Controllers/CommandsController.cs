using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommanderAPI.Data;
using CommanderAPI.Models;
using System;

namespace CommanderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        public CommandsController(ICommandAPIRepo repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            IEnumerable<Command> commandItems = _repository.GetAllCommands();

            return Ok(commandItems);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            Command commandItem = _repository.GetCommandById(id);

            if (commandItem is null) return NotFound();

            return Ok(commandItem);
        }




    }
}

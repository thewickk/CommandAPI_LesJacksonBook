﻿using AutoMapper;
using CommanderAPI.Dtos;
using CommanderAPI.Models;

namespace CommanderAPI.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>();

            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();

            CreateMap<Command, CommandUpdateDto>();
        }
    }
}

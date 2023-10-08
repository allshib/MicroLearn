using AutoMapper;
using Micro.CommandService.Dtos;
using Micro.CommandService.Models;

namespace Micro.CommandService.Profiles
{
    public class CommandProfile : Profile
    {


        public CommandProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            //CreateMap<PlatformReadDto, Platform>();

            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
        }
    }
}

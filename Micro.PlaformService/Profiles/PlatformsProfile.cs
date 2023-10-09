using AutoMapper;
using Micro.PlaformService.Dtos;
using Micro.PlaformService.Models;

namespace Micro.PlaformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile() {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();

        }
    }
}

using Micro.PlaformService.Dtos;

namespace Micro.PlaformService.SyncDataServicesHttp
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}

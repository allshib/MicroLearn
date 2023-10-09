using Micro.PlaformService.Dtos;

namespace Micro.PlaformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);

    }
}

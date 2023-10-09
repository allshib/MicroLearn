using Micro.PlaformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Micro.PlaformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() 
            { 
                HostName = _configuration["RabbitMQHost"], 
                Port = int.Parse(_configuration["RabbitMQPort"]) 
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("Установлено соединение с шиной сообщений");
            }
            catch (Exception e)
            {

                Console.WriteLine($"Не удалось подключиться к шине сообщений: {e.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"Соединение RabbitMQ отключено");
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message  = JsonSerializer.Serialize(platformPublishedDto);

            if(_connection.IsOpen)
            {
                Console.WriteLine("Соединение RabbitMQ открыто");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("Соединение RabbitMQ закрыто");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body);

            Console.WriteLine($"Отправлено: {message}");
        }
        public void Dispose()
        {

            if(_channel.IsOpen)
            {
                Console.WriteLine("Закрываю подключение к RabbitMQ");
                _channel.Close();
                _connection.Close();
            }
        }
    }
}

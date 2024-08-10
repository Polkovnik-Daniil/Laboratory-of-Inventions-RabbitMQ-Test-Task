using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Laboratory_of_Inventions_RabbitMQ.Services.Background
{
    public class RabbitBackgroundWorker : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;

        public RabbitBackgroundWorker()
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "TestQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consume;

            _channel.BasicConsume("TestQueue", false, consumer);

            return Task.CompletedTask;
        }

        private void Consume(object? ch, BasicDeliverEventArgs eventArgs)
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}

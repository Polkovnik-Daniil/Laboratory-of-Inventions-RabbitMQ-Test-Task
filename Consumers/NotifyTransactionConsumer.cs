using Laboratory_of_Inventions_RabbitMQ.Database.Entity;
using MassTransit;

namespace Laboratory_of_Inventions_RabbitMQ.Consumers
{
    public class NotifyTransactionConsumer : IConsumer<DeviceStatus>
    {
        private readonly ILogger<NotifyTransactionConsumer> _logger;

        public NotifyTransactionConsumer(ILogger<NotifyTransactionConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DeviceStatus> context)
        {
            _logger.LogInformation($"Consume notify about transaction {context.Message} amount {context.Message}");
            //todo: do something with data

        }
    }
}

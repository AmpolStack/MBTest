using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WriterApi.Hosted;

public class MessageReciever : BackgroundService
{
    private readonly IConnection _connection;
    private IChannel _channel;
    private readonly ILogger<MessageReciever> _logger;

    public MessageReciever(IConnection connection, ILoggerFactory loggerFactory)
    {
        _connection = connection;
        _logger = loggerFactory.CreateLogger<MessageReciever>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
        var queue = await _channel.QueueDeclareAsync("temp1", durable: false, exclusive: false, autoDelete: false,
            arguments: null, cancellationToken: stoppingToken);
        
        await _channel.ExchangeDeclareAsync("principal", ExchangeType.Direct, cancellationToken: stoppingToken);
        await _channel.QueueBindAsync("temp1", "principal", "info", cancellationToken: stoppingToken);
        var subscriber = new AsyncEventingBasicConsumer(_channel);
        var counter = 0;
        subscriber.ReceivedAsync += async (sender, message) =>
        {
            counter++;
            var body = message.Body.ToArray();
            var messageDecoded = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Message [{counter}] : Received: {messageDecoded}");
            await _channel.BasicAckAsync(message.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            _logger.LogInformation($"Message [{counter}] : Acked");
        };

        await _channel.BasicConsumeAsync(queue: "temp1", autoAck: false, consumer: subscriber, cancellationToken: stoppingToken);

    }
}
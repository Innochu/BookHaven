using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public IModel GetChannel()
    {
        return _channel;
    }

    public void CloseConnection()
    {
        _channel.Close();
        _connection.Close();
    }

    public void DeclareQueue(string queueName, bool durable = false, bool exclusive = false, bool autoDelete = false)
    {
        _channel.QueueDeclare(queue: queueName, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: null);
    }

    public void DeclareExchange(string exchangeName, string type = ExchangeType.Direct, bool durable = false, bool autoDelete = false)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: type, durable: durable, autoDelete: autoDelete, arguments: null);
    }

    public void BindQueueToExchange(string queueName, string exchangeName, string routingKey = "")
    {
        _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
    }

    public void PublishMessage(string exchangeName, string routingKey, byte[] body)
    {
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
    }

    public void ConsumeQueue(string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
    {
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += receivedHandler;
        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}

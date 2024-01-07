using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace BookHaven.Messaging
{
    public class InventoryUpdater
    {
        private readonly IModel _channel;

        public InventoryUpdater(RabbitMQService rabbitMQService)
        {
            _channel = rabbitMQService.GetChannel();
        }

        public void StartListening()
        {
            _channel.QueueDeclare(queue: "new_book_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received new book notification: {message}");

                // Add logic to update inventory based on the new book
            };

            _channel.BasicConsume(queue: "new_book_queue", autoAck: true, consumer: consumer);
        }

       
    }
}

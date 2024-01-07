using RabbitMQ.Client;
using System.Text;

namespace BookHaven.Messaging
{
    public class BookPublisher
    {
        private readonly IModel _channel;

        public BookPublisher(RabbitMQService rabbitMQService)
        {
            _channel = rabbitMQService.GetChannel();
        }

        public void PublishNewOrder(string bookTitle)
        {
            _channel.BasicPublish(exchange: "", routingKey: "new_book_queue", basicProperties: null, body: Encoding.UTF8.GetBytes(bookTitle));
        }
    }
}

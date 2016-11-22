using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using log4net;

namespace RabbitMQConsole1
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {            
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "localhost";
            connectionFactory.UserName = "RabbitMQPOC";
            connectionFactory.Password = "teste";

            //EventingBasicConsumer_LISTENER(connectionFactory);
            EventingBasicConsumer_LISTENER1(connectionFactory);
            //Subscription(connectionFactory);

            Console.ReadKey();
        }

        private static void EventingBasicConsumer_LISTENER(ConnectionFactory factory)
        {
            string messageContent = string.Empty;
            string queueName = "QueueBradescoAxPedidoCentral";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);                    

                    consumer.Received += (o, e) =>
                    {
                        messageContent = Encoding.UTF8.GetString(e.Body);

                        Console.WriteLine(messageContent);

                        Logger.Error(messageContent);                        
                    };

                    channel.BasicConsume(queueName, true, consumer);                    
                }
            }
        }

        private static void EventingBasicConsumer_LISTENER1(ConnectionFactory factory)
        {
            try
            {
                string messageContent = string.Empty;
                string queueName = "QueueBradescoAxPedidoCentral";
                string exchangeName = "ExchangeBradescoAxPedidoCentral";

                const int NUMBER_OF_WORKROLES = 3;

                IConnection connection = factory.CreateConnection();

                //Cria a canal de comunicação com a rabbit mq
                IModel channel = connection.CreateModel();

                for (int i = 0; i < NUMBER_OF_WORKROLES; i++)
                {
                    Task.Factory.StartNew(() =>
                    {
                        lock (channel)
                        {
                            //channel.QueueDeclare(queueName, true, false, false, null);
                            channel.QueueBind(queueName, exchangeName, "", new Dictionary<string, object>());

                            var consumer = new EventingBasicConsumer(channel);
                            consumer.ConsumerTag = Guid.NewGuid().ToString();

                            consumer.Received += (o, e) =>
                            {
                                messageContent = Encoding.UTF8.GetString(e.Body);

                                Console.WriteLine(messageContent);

                                Logger.Error(messageContent);

                                //Diz ao RabbitMQ que a mensagem foi lida com sucesso pelo consumidor
                                channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: true);
                            };

                            //Registra o consumidor no RabbitMQ
                            channel.BasicConsume(queueName, noAck: false, consumer: consumer);

                            channel.QueueUnbind(queueName, exchangeName, "", new Dictionary<string, object>());
                        }
                    });
                }
            }
            catch (BrokerUnreachableException bex)
            {
                Exception ex = bex;

                while (ex != null)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("inner:");

                    ex = ex.InnerException;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }            
        }

        private static void Subscription(ConnectionFactory connectionFactory)
        {
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    var subscription = new RabbitMQ.Client.MessagePatterns.Subscription(model, "QueueBradescoAxPedidoCentral", false);
                    while (true)
                    {
                        BasicDeliverEventArgs basicDeliveryEventArgs = subscription.Next();
                        string messageContent = Encoding.UTF8.GetString(basicDeliveryEventArgs.Body);
                        Console.WriteLine(messageContent);

                        Logger.Error(messageContent);

                        subscription.Ack(basicDeliveryEventArgs);
                    }
                }
            }
        }
    }
}

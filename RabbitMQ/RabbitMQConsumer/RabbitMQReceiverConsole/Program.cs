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

            //LOCAL        
            //connectionFactory.Uri = "amqp://10.44.12.213:5672";
            ////connectionFactory.Endpoint.HostName = "amqp://10.44.12.213:5672";
            ////connectionFactory.HostName = "localhost";
            //connectionFactory.VirtualHost = "/AxPedidoCentral";
            //connectionFactory.UserName = "RabbitMQAxPedidoCentral";
            //connectionFactory.Password = "axpedidocentral";

            //DEV
            //connectionFactory.Uri = "amqp://10.33.170.162:5672";
            //connectionFactory.VirtualHost = "/AxPedidoCentral";
            //connectionFactory.UserName = "RabbitMQAxPedidoCentral_DEV";
            //connectionFactory.Password = "axpedidocentral_dev";

            //HOM
            connectionFactory.Uri = "amqp://10.33.170.162:5672";
            connectionFactory.VirtualHost = "AxPedidoCentral_HOM";
            connectionFactory.UserName = "RabbitMQAxPedidoCentral_HOM";
            connectionFactory.Password = "axpedidocentral_hom";

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

                //LOCAL
                //string exchange = "ExchangeAxPedidoCentralBradesco_LOCAL";
                ////string queue = "QueueAxPedidoCentraBradescoAutorizacao_LOCAL";
                //string queue = "QueueAxPedidoCentraBradescoConfirmacao_LOCAL";

                //DEV
                //string exchange = "ExchangeAxPedidoCentralBradesco_LOCAL1";
                //string queue = "QueueTeste1";

                //HOM
                string exchange = "ExchangeAxPedidoCentralGenerali_HOM";
                string queue = "QueueAxPedidoCentralGeneraliConfirmacao_HOM";

                //string queueName = "QueueBradescoAxPedidoCentral";
                //string exchangeName = "ExchangeBradescoAxPedidoCentral";

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
                            channel.QueueBind(queue, exchange, "", new Dictionary<string, object>());

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
                            channel.BasicConsume(queue, noAck: false, consumer: consumer);

                            channel.QueueUnbind(queue, exchange, "", new Dictionary<string, object>());
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

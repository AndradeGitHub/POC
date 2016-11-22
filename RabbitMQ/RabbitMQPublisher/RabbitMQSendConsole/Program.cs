using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Newtonsoft.Json;

namespace RabbitMQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {                     
            ConnectionFactory connectionFactory = new ConnectionFactory();            
            connectionFactory.Endpoint.HostName = "amqp://10.44.13.74:5672";
            //connectionFactory.HostName = "localhost";
            connectionFactory.VirtualHost = "/AxPedidoCentral";
            connectionFactory.UserName = "RabbitMQPOC";
            connectionFactory.Password = "teste";            

            BasicPublish(connectionFactory);            
        }

        private static void BasicPublish(ConnectionFactory connectionFactory)
        {
            try
            {
                //string exchange = "ExchangeAxPedido";
                //string queue = "QueueBradescoAxPedidoCentral";

                string exchange = "ExchangeAxPedidoCentralBradesco_LOCAL1";
                string queue = "QueueTeste1";

                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    using (IModel model = connection.CreateModel())
                    {
                        //model.ExchangeDeclare(exchange, ExchangeType.Direct, true, false);
                        //model.QueueDeclare(queue, true, false, false, null);
                        model.QueueBind(queue, exchange, "", new Dictionary<string, object>());                        

                        for (int i = 0; i < 5000; i++)
                        {
                            string message = string.Concat("Hello!! + ", i);

                            var json = JsonConvert.SerializeObject(message);
                            
                            IBasicProperties basicProperties = model.CreateBasicProperties();
                            basicProperties.ContentType = "application/json";
                            basicProperties.Persistent = true;
                            basicProperties.DeliveryMode = 2;

                            model.BasicPublish(exchange, "", false, basicProperties, Encoding.UTF8.GetBytes(json));                            
                        }

                        model.QueueUnbind(queue, exchange, "", new Dictionary<string, object>());
                    }
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
    }
}
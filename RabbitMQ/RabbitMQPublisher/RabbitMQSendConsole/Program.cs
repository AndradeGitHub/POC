using System;
using System.Collections.Generic;
using System.Text;

using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Newtonsoft.Json;

namespace RabbitMQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {                     
            ConnectionFactory connectionFactory = new ConnectionFactory();

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

            //for (int i = 0; i < 50000; i++)
            //{
                BasicPublish(connectionFactory);
            //}
        }

        private static void BasicPublish(ConnectionFactory connectionFactory)
        {
            try
            {
                //LOCAL
                //string exchange = "ExchangeAxPedidoCentralBradesco_LOCAL";
                //string queue = "QueueAxPedidoCentraBradescoAutorizacao_LOCAL";
                //string queue = "QueueAxPedidoCentraBradescoConfirmacao_LOCAL";

                //DEV
                //string exchange = "ExchangeAxPedidoCentralBradesco_LOCAL1";
                //string queue = "QueueTeste1";

                //HOM
                string exchange = "ExchangeAxPedidoCentralGenerali_HOM";
                string queue = "QueueAxPedidoCentralGeneraliConfirmacao_HOM";

                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    using (IModel model = connection.CreateModel())
                    {
                        //model.ExchangeDeclare(exchange, ExchangeType.Direct, true, false);
                        //model.QueueDeclare(queue, true, false, false, null);
                        model.QueueBind(queue, exchange, "", new Dictionary<string, object>());

                        IBasicProperties basicProperties = model.CreateBasicProperties();
                        basicProperties.ContentType = "application/json";
                        basicProperties.Persistent = true;
                        basicProperties.DeliveryMode = 2;

                        for (int i = 0; i < 50000; i++)
                        {
                            string message = string.Concat("Hello!! + ", i);

                            var json = JsonConvert.SerializeObject(message);
                            
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
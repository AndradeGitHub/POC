using System;

using eventos.domain;

namespace eventos.console
{
    class Program
    {        
        static void Main(string[] args)
        {
            Carro carro = new Carro();
            carro.ExcedeuVelocidadeSeguranca += new VelocidadeSegurancaExcedidaEventHandler(CarroLimiteVelocidadeExcedida);
            
            for (int i = 0; i < 3; i++)
            {
                carro.Acelerar(30);
                Console.WriteLine(" Velocidade atual : {0} Kmh ", carro.Velocidade);
            }

            Console.ReadKey();            
        }

        static void CarroLimiteVelocidadeExcedida(object source, EventArgs e)
        {
            Carro carro = (Carro)source;
            Console.WriteLine("O limite de velocidade foi excedido ({0}Kmh)", carro.Velocidade);
        }
    } 
}
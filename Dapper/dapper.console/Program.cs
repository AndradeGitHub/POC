using System;
using System.Collections.Generic;
using System.Configuration;

using dapper.domain;
using dapper.domain.model;

namespace dapper.console
{
    class Program
    {
        static string _strConexao = ConfigurationManager.ConnectionStrings["Audatex"].ConnectionString;

        static void Main(string[] args)
        {
            Repository repo = new Repository(_strConexao);

            //Consulta Simples
            var result = repo.ConsultaSimples();            
            foreach (dynamic usuario in result)
                Console.WriteLine(string.Concat("CPF: ", usuario.CPF, " - NOME: ", usuario.Nome));

            //Consulta Simples Tipada
            var result1 = repo.ConsultaSimplesTipada();
            foreach (dynamic usuario in result1)
                Console.WriteLine(string.Concat("CPF: ", usuario.CPF, " - NOME: ", usuario.Nome));

            //Consulta Simples Tipada1
            var result2 = repo.ConsultaSimplesTipada1();            
            foreach (Usuario usuario in result2)
                Console.WriteLine(string.Concat("CPF: ", usuario.CPF, " - NOME: ", usuario.Nome));

            //Consulta Multipla
            var result3 = repo.ConsultaMultipla();
            foreach (dynamic usuario in result3)
                Console.WriteLine(string.Concat("CPF: ", usuario.CPF, " - NOME: ", usuario.Nome));

            //Insert
            var result4 = repo.Add();
            if (result4 > 0)
                Console.WriteLine("Inserção Executada");

            //Update
            var result5 = repo.Update();
            if (result5 > 0)
                Console.WriteLine("Atualização Executada");

            Console.ReadLine();
        }
    }
}

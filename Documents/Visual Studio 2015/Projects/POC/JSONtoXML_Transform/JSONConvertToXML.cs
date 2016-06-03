using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Xml.Linq;
using Newtonsoft.Json;

namespace JSONtoXML
{
    public class JSONConvertToXML
    {
        public static void Convert()
        {
            //LEITURA DO JSON PARA CONVERSÃO XML
            var data = new
            {
                Data = new
                {
                    Dia = 8,
                    Mes = 4,
                    Ano = 2016,
                    RepresentacaoTexto = "2016-04-08",
                    Semestre = 1,
                    Trimestre = 1,
                    Estacao = "Outono"
                }
            };

            //string JSON = JsonConvert.SerializeObject(data);

            //LEITURA DO ARQUIVO JSON PARA CONVERSÃO XML
            string JSON = File.ReadAllText(@"C:\\Users\\andre.andrade\\Documents\\Visual Studio 2015\\Projects\\JSONtoXML_Transform\Files\CatalogoProdutoFull.json");

            Console.WriteLine($"Valor no formato JSON: {JSON}");

            XDocument documentoXML = JsonConvert.DeserializeXNode(JSON);
            Console.WriteLine("\nValor convertido para XML:");
            Console.WriteLine(documentoXML.ToString());

            string valorFinal = JsonConvert.SerializeXNode(documentoXML);
            Console.WriteLine($"\nConvertendo de XML para JSON: {valorFinal}");

            Console.WriteLine();            
        }
    }
}

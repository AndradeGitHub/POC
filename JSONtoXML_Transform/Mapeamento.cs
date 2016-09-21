using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using Newtonsoft.Json;
using AutoMapper;

namespace JSONtoXML
{
    public class Mapeamento
    {
        public static void Mapear()
        {
            string jsonArchive = File.ReadAllText(@"C:\\Users\\andre.andrade\\Documents\\Visual Studio 2015\\Projects\\JSONtoXML_Transform\Files\CatalogoProdutoFull.json");            

            //Converte JSON para XML
            XDocument documentoXML = JsonConvert.DeserializeXNode(jsonArchive);
            Console.WriteLine("\nValor convertido para XML:");
            Console.WriteLine(documentoXML.ToString());

            // Criando o Mapeamento por demanda.            
            Mapper.CreateMap<XDocument, List<Products>>();

            // Transformando a Model Cliente em ClienteViewModel
            var map = Mapper.Map<XDocument, List<Products>>(documentoXML);            

           // Console.WriteLine($"Valor no formato JSON Convertido: {map.productId}");


            //TESTE COM A ENTRADA JSON
            var rootObjectJson = new
            {
                RootObject = new
                {
                    Product = new
                    {
                        productId = 1,
                        name = "Teste Name",
                        description = "Teste Description"
                    }
                }
            };

            string output = JsonConvert.SerializeObject(rootObjectJson);
            RootObject deserializedProduct = JsonConvert.DeserializeObject<RootObject>(output);





            //string json = File.ReadAllText(@"C:\\Users\\andre.andrade\\Documents\\Visual Studio 2015\\Projects\\JSONtoXML_Transform\Files\CatalogoProdutoFull.json");
            string json = "{\"productId\": 1, \"name\": \"Abdullah\"}";
            //Products1 user = JsonConvert.DeserializeObject<Products1>(json);

            // Criando o Mapeamento por demanda.
            Mapper.CreateMap<string, ProductTest>();

            // Transformando a Model Cliente em ClienteViewModel
            var map1 = Mapper.Map<string, ProductTest>(json);
            map1.adtional = "Teste Adtional";

            //Converte JSON para XML
            XDocument documentoXML1 = JsonConvert.DeserializeXNode(map1.ToString());
            Console.WriteLine("\nValor convertido para XML:");
            Console.WriteLine(documentoXML1.ToString());




            string JSON = JsonConvert.SerializeObject(rootObjectJson);
            Console.WriteLine($"Valor no formato JSON: {JSON}");

            // Criando o Mapeamento por demanda.
            Mapper.CreateMap<string, ProductTest>();

            // Transformando a Model Cliente em ClienteViewModel
            var map2 = Mapper.Map<string, ProductTest>(JSON);
            map2.adtional = "Teste Adtional";
        }
    }
}

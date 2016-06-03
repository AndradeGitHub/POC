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
    public class MapeamentoEntradaJSON
    {
        public static void Mapear()
        {            
            string json = File.ReadAllText(@"C:\\Users\\andre.andrade\\Documents\\Visual Studio 2015\\Projects\\JSONtoXML_Transform\Files\CatalogoProdutoFull.json");
            RootObject entidade = JsonConvert.DeserializeObject<RootObject>(json);
            entidade.products[0].adtional = "Teste Adtional";
                   
            //Converte para XML
            string JSON = JsonConvert.SerializeObject(entidade);            
            XDocument documentoXML = JsonConvert.DeserializeXNode(JSON, "CATALOGO", true);
            Console.WriteLine("\nValor convertido para XML:");
            Console.WriteLine(documentoXML.ToString());

            //XmlNode myXmlNode = JsonConvert.DeserializeXmlNode(JSON, "root");
            //Console.WriteLine(myXmlNode.InnerXml);    
        }
    }
}

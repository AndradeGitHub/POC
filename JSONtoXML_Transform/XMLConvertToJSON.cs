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
    public class XMLConvertToJSON
    {
        public static void Convert()
        {
            XDocument documentoXML = XDocument.Load(@"C:\\Users\\andre.andrade\\Documents\\Visual Studio 2015\\Projects\\JSONtoXML_Transform\Files\PadraoDotz.xml");
            Console.WriteLine("\nValor do XML:");
            Console.WriteLine(documentoXML.ToString());

            string valorFinalXML = JsonConvert.SerializeXNode(documentoXML);
            Console.WriteLine($"\nConvertendo de XML para JSON: {valorFinalXML}");

            XDocument documentoXML2 = JsonConvert.DeserializeXNode(valorFinalXML);
            Console.WriteLine("\nValor convertido para XML:");
            Console.WriteLine(documentoXML2.ToString());
        }
    }
}

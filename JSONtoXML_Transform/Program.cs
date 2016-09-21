using System;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace JSONtoXML
{
    class Program
    {
        static void Main(string[] args)
        {
            //JSONConvertToXML.Convert();
            //XMLConvertToJSON.Convert();
            
            //MapeamentoEntidade.Mapear();
            MapeamentoEntradaJSON.Mapear();

            Console.ReadLine();
        }
    }
}

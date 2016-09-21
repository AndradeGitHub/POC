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
    public class MapeamentoEntidade
    {
        public static void Mapear()
        {
            var produtcs = new Product
            {
                productId = 1,
                name = "Teste Name",
                description = "Teste Description"
            };
            
            Mapper.CreateMap<Product, ProductTest>();
            
            var map = Mapper.Map<Product, ProductTest>(produtcs);
            map.adtional = "Teste Adtional";
        }
    }
}

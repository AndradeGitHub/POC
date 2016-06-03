using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONtoXML
{  
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ProductTest
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string adtional { get; set; }
    }
    


    public class Products
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string adtional { get; set; }
    }

    public class RootObject
    {
        public string creationDate { get; set; }
        public List<Products> products { get; set; }        
    }
}
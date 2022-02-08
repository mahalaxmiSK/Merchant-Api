using System.Collections.Generic;

namespace AspWebClient.Model
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; }
        public string Response { get; set; }
    }
    public class Product
    {
        public string ProductName { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
    }
}

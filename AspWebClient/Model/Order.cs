using System;
using System.Collections.Generic;

namespace AspWebClient.Model
{
    public class OrderResponse
    {
        public List<Order> Orders { get; set; }
        public string Response { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public string GlobalChannelName { get; set; }
        public string ChannelOrderNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

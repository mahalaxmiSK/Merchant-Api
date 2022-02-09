using AspWebClient.Model;
using Merchant.BL;
using System;
using System.Collections.Generic;

namespace AspWebClient.Utilities
{
    
    public static class ApiUtility
    {
        public static OrderResponse GetOrders()
        {
            var orderResponse = new OrderResponse();
            orderResponse.Orders = new List<Order>();
            try
            {
                var apiOrders = MerchantApi.ApiHelper.GetOrdersWithStatus().Result;
                foreach (var item in apiOrders.Content)
                {
                    var orderItem = new Order
                    {
                        ChannelOrderNo = item.ChannelOrderNo,
                        Email = item.Email,
                        GlobalChannelName = item.GlobalChannelName,
                        Id = item.Id,
                        MerchantOrderNo = item.MerchantOrderNo,
                        OrderDate = item.OrderDate,
                        Phone = item.Phone,
                        Status = item.Status,
                        Products = new List<Product>()
                    };

                    item.Lines.ForEach(_ => orderItem.Products.Add(new Product { Gtin = _.Gtin, ProductName = _.Description, Quantity = _.Quantity }));
                    orderResponse.Orders.Add(orderItem);
                }
                return orderResponse;
            }
            catch (Exception ex)
            {
                orderResponse.Response = $"Error during get order. {ex.Message}";
                return orderResponse;
            }
        }
        public static ProductResponse GetTop5Products()
        {
            var productResponse = new ProductResponse();
            productResponse.Products = new List<Product>();
            try
            {
                var apiTopProducts = MerchantApi.ApiHelper.GetTop5Products();
                apiTopProducts.ForEach(_ => productResponse.Products.Add(new Product { Gtin = _.Gtin, ProductName = _.ProductName, Quantity = _.Quantity }));
                return productResponse;
            }
            catch (Exception ex)
            {
                productResponse.Response = $"Error during get top 5 products. {ex.Message}";
                return productResponse;
            }
        }
        public static IEnumerable<string> GetProductsNo()
        {
            var productsNo = new List<string>();
            try
            {
                var apiOrders = MerchantApi.ApiHelper.GetOrdersWithStatus().Result;
                apiOrders?.Content.ForEach(_ => _.Lines.ForEach(x =>
                {
                    if (!productsNo.Contains(x.MerchantProductNo))
                        productsNo.Add(x.MerchantProductNo);
                }));
                return productsNo;
            }
            catch (Exception ex)
            {
                //return empty list
                return productsNo;
            }
        }
        public static UpdateStockResponse UpdateStock(string MerchantProductNo,int stockLocationId)
        {
            UpdateStockResponse response = new UpdateStockResponse();
            try
            {
                response.Response = MerchantApi.ApiHelper.UpdateStock(MerchantProductNo, 25, stockLocationId);
                return response;
            }
            catch (Exception ex)
            {
                response.Response = ex.Message;
                return response;
            }
        }
    }
}

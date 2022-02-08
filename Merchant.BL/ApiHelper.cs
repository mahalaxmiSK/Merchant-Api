using Merchant.BL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Merchant.BL.ApiHandlers;
namespace Merchant.BL
{
    public class ApiHelper
    {
        private IApiHandler myApiHandler;
        private Orders orderList;

        public ApiHelper(IApiHandler apiHandler)
        {
            myApiHandler = apiHandler;
            orderList = new Orders();
        }

        /// <summary>
        /// Gets Orders with IN_PROGRESS Status
        /// </summary>
        /// <returns></returns>
        public async Task<Orders> GetOrdersWithStatus()
        {
            try
            {
                var response = await myApiHandler.FetchOrdersWithInProgressStatus();
                if (response != null && response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Orders>(jsonString);
                    orderList = data;
                    return data;
                }
                else
                {
                    throw new Exception($"Error in fetching data. code :{response?.StatusCode} ,Reason : {response.ReasonPhrase}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error in fetching data: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets top 5 products sold from orders
        /// </summary>
        /// <returns></returns>
        public List<Product> GetTop5()
        {
            var returnList = new List<Product>();
            var data = orderList.Content.SelectMany(_ => _.Lines);
            if (orderList == null || orderList.Content == null || orderList.Content.Count <= 0)
                throw new Exception($"Error: No orders present");
            foreach (var order in data)
            {
                if (returnList.Any(_ => _.ProductName == order.Description))
                {
                    returnList.First(_ => _.ProductName == order.Description).Quantity += order.Quantity;
                }
                else
                {
                    returnList.Add(new Product { ProductName = order.Description, Gtin = order.Gtin, Quantity = order.Quantity });
                }
            }
            return returnList.OrderByDescending(_ => _.Quantity).Take(5).ToList();
        }

        /// <summary>
        /// Updates stock to 25
        /// </summary>
        /// <param name="MerchantProductNo"></param>
        /// <param name="stock"></param>
        /// <param name="stockLocationId"></param>
        /// <returns></returns>
        public string UpdateStock(string MerchantProductNo, int stock, int stockLocationId)
        {
            try
            {
                var response = myApiHandler.UpdateStockTo25(MerchantProductNo, stock, stockLocationId).Result;
                if (response.IsSuccessStatusCode)
                    return "Stock updated successfully";
                else
                    return ($"Error during update stock. code :{response?.StatusCode} ,Reason : {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error during update stock: {ex.Message}");
            }
        }
    }
}
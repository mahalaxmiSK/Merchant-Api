using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Merchant.BL.ApiHandlers
{
    public class ApiHandler : IApiHandler
    {
        private readonly string baseurl = "https://api-dev.channelengine.net/api/v2/";
        private readonly string apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        public async Task<HttpResponseMessage> FetchOrdersWithInProgressStatus()
        {
            string url = $"{baseurl}orders?statuses=IN_PROGRESS&apikey={apiKey}";
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> UpdateStockTo25(string MerchantProductNo, int stock, int stockLocationId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{baseurl}offer?apikey={apiKey}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");

                    request.Content = new StringContent($"[{{\"MerchantProductNo\":\"{MerchantProductNo}\",\"Stock\":{stock},\"StockLocationId\":{stockLocationId}}}]");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-patch+json");

                    return await httpClient.SendAsync(request);
                }
            }
        }
    }
}

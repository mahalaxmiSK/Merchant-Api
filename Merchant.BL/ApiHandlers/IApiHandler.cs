using System.Net.Http;
using System.Threading.Tasks;

namespace Merchant.BL.ApiHandlers
{
    public interface IApiHandler
    {
        Task<HttpResponseMessage> FetchOrdersWithInProgressStatus();
        Task<HttpResponseMessage> UpdateStockTo25(string MerchantProductNo, int stock, int stockLocationId);
    }
}

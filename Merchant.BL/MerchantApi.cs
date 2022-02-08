using Merchant.BL.ApiHandlers;

namespace Merchant.BL
{
    public static class MerchantApi
    {
        private static IApiHandler apiHandler;
        public static ApiHelper ApiHelper { get; set; }
        static MerchantApi()
        {
            apiHandler = new ApiHandler();
            ApiHelper = new ApiHelper(apiHandler);
        }
    }
}

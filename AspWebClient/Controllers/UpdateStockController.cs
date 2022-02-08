using System.Collections.Generic;
using AspWebClient.Model;
using AspWebClient.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AspWebClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateStockController : Controller
    {
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<string>> GetProductsNo()
        {
            return ApiUtility.GetProductsNo() as List<string>;
        }
        [HttpGet("[action]/{merchantproductno}/{stocklocationid}")]
        public ActionResult<UpdateStockResponse> UpdateStockTo25(string MerchantProductNo, int stockLocationId)
        {
            return ApiUtility.UpdateStock(MerchantProductNo, stockLocationId);
        }
    }
}
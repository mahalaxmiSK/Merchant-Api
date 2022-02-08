using AspWebClient.Model;
using AspWebClient.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AspWebClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        public ActionResult<OrderResponse> GetOrders()
        {
            return ApiUtility.GetOrders();
        }
        [HttpGet("[action]")]
        public ActionResult<ProductResponse> GetTop5Products()
        {
            return ApiUtility.GetTop5Products();
        }
    }
}

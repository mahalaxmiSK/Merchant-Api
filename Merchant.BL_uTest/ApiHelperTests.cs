
using Merchant.BL;
using Merchant.BL.ApiHandlers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.BL_uTest
{
    public class ApiHelperTests
    {
        Mock<IApiHandler> apiHandlerMock = new Mock<IApiHandler>();
        [Test]
        public void GetOrdersWithStatus_SuccessStatus_ValidteData()
        {
            //Arrange
            var orderHttpResponseMessage = Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(TestData.orderJson, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            });
            apiHandlerMock.Setup(_ => _.FetchOrdersWithInProgressStatus()).Returns(orderHttpResponseMessage);
            var apiHelper = new ApiHelper(apiHandlerMock.Object);

            //Act
            var orders = apiHelper.GetOrdersWithStatus().Result;
            //Assert
            Assert.IsTrue(orders.Content.Count == 3);
            Assert.IsTrue(orders.Content[1].Lines.Count == 3);
        }
        [Test]
        public void GetOrdersWithStatus_ResponseError_ExceptionThrown()
        {
            //Arrange
            var orderHttpResponseMessage = Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            });
            apiHandlerMock.Setup(_ => _.FetchOrdersWithInProgressStatus()).Returns(orderHttpResponseMessage);
            var apiHelper = new ApiHelper(apiHandlerMock.Object);

            //Act //Assert
            Assert.Throws<AggregateException>(()=>apiHelper.GetOrdersWithStatus().Wait());
            
        }

        [Test]
        public void GetTop5Products_ValidateData()
        {
            //Arrange
            var orderHttpResponseMessage = Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(TestData.orderJson, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            });
            apiHandlerMock.Setup(_ => _.FetchOrdersWithInProgressStatus()).Returns(orderHttpResponseMessage);
            var apiHelper = new ApiHelper(apiHandlerMock.Object);
            apiHelper.GetOrdersWithStatus().Wait();
            
            //Act
            var products = apiHelper.GetTop5Products();
            
            //Assert
            Assert.IsTrue(products.Count == 5);
            var expectedProducts = products.OrderByDescending(_ => _.Quantity);
            Assert.IsTrue(expectedProducts.SequenceEqual(products));
        }
    }
}

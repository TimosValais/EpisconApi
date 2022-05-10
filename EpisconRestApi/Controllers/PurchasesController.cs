using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EpisconApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private PurchaseService _purchaseService;

        public PurchasesController(StoreContext context)
        {
            _purchaseService = new PurchaseService(context);
        }

        [HttpPost]
        [Route("New/{productId}/{userId}")]
        public HttpResponseMessage New(int productId, int userId)
        {
            try
            {
                _purchaseService.New(productId, userId);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.Message));
                return response;
            }
        }

        [HttpGet]
        [Route("GetPurchasesByUser/{userId}")]
        public object GetByUserId(int userId)
        {
            try
            {
                List<Purchase> purchases = _purchaseService.GetByUser(userId).ToList();
                return purchases;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}

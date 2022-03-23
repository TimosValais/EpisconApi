using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpisconApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetAll()
        {
            return "OK.";
        }
    }
}

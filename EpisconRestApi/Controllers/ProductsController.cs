using EpisconApi.DBContexts;
using EpisconApi.Helpers;
using EpisconApi.Models;
using EpisconApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;

namespace EpisconApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private ProductService _productService;

        public ProductsController(StoreContext context)
        {
            _productService = new ProductService(context);
        }
        [HttpGet]
        [Route("{limit?}/{sortby?}/{orderby?}")]
        public async Task<List<Product>> GetAll(int limit = 200, string? sortBy = "Title", string? orderBy = "desc")
        {
            try
            {
                List<Product> products = await _productService.GetAll(limit, sortBy, orderBy);
                return products;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpGet("All")]
        public async Task<IActionResult> GetFromQuery([FromQuery] ProductQueryParameters queryParameters)
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetFromQuery(queryParameters);
                return Ok(products.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchQueryParameters queryParameters)
        {
            try
            {
                IEnumerable<Product> products = await _productService.Search(queryParameters);
                return Ok(products.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("SeedInitialData")]
        public async Task<HttpResponseMessage> SeedInitialData()
        {

            bool response = await _productService.SeedInitialData();
            if(response) return new HttpResponseMessage(HttpStatusCode.OK);
            
            
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("GetProductsFromUser/{userId}")]
        [HttpGet]
        public object GetProductsFromUser(int userId)
        {
            try
            {
                List<Product> products = _productService.GetProductsFromUser(userId);
                return products;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("Add")]
        public IActionResult Create(Product newProduct)
        {
            try
            {
                _productService.Create(newProduct);
                return Ok($"Product was created {newProduct.ProductId}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            try
            {
                _productService.Update(id, product);
                return Ok($"Product was updated {JsonConvert.SerializeObject(_productService.GetById(id))}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return Ok("Item was deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteRange")]
        public async Task<IActionResult> DeleteRange([FromQuery] int[] ids)
        {
            try
            {
                IEnumerable<Product> products = _productService.DeleteRange(ids);

                return Ok($"These products were deleted {JsonConvert.SerializeObject(products)}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

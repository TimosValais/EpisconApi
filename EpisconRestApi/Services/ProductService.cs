using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Repositories;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace EpisconApi.Services
{
    public class ProductService
    {

        private ProductRepo _productRepo;
        private PurchaseService _purchaseService;


        private const string _productApiUrl = "https://fakestoreapi.com/products/";

        public ProductService(StoreContext context)
        {
            _productRepo = new ProductRepo(context);
            _purchaseService = new PurchaseService(context);

        }

        public async Task<bool> SeedInitialData()
        {
            IEnumerable<Product> productsFromApi = await GetAllFromApi();
            try
            {
                _productRepo.UpdateFromRange(productsFromApi);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Product GetById(int productId)
        {

            return _productRepo.GetById(productId);
        }

        public async Task<List<Product>> GetAll(int limit, string sortBy, string orderBy)
        {
            //IEnumerable<Product> productsFromApi = await GetAllFromApi();
            //List<Product> products = productsFromApi.ToList();
            PropertyInfo[] propInfos = typeof(Product).GetProperties();
            PropertyInfo propertyToSort = null;
            foreach (PropertyInfo propertyInfo in propInfos)
            {
                if (propertyInfo.Name.ToUpper() == sortBy.ToUpper())
                {
                    propertyToSort = propertyInfo;
                    break;
                }
            }
            List<Product> products = _productRepo.GetAll(limit, propertyToSort.Name, orderBy).ToList();

            return products;

        }
        public List<Product> GetUsersFromProduct(int userId)
        {
            List<int> productsIds = new List<int>();
            List<Purchase> purchases = _purchaseService.GetByProduct(userId).ToList();
            foreach (var purchase in purchases)
            {
                if (!productsIds.Any(productId => productId == purchase.ProductId))
                {
                    productsIds.Add(purchase.ProductId);
                }
            }
            return GetByIds(productsIds);
        }
        public List<Product> GetByIds(List<int> ids)
        {
            return _productRepo.GetByIds(ids);
        }

        private async Task<IEnumerable<Product>> GetAllFromApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();




            HttpResponseMessage response = await httpClient.GetAsync(_productApiUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseContent);
            return products;
        }

    }
}

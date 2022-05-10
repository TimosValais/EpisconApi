using EpisconApi.DBContexts;
using EpisconApi.Helpers;
using EpisconApi.Models;
using EpisconApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace EpisconApi.Services
{
    public class ProductService
    {

        private ProductRepo _productRepo;
        private PurchaseRepo _purchaseRepo;


        private const string _productApiUrl = "https://fakestoreapi.com/products/";

        public ProductService(StoreContext context)
        {
            _productRepo = new ProductRepo(context);
            _purchaseRepo = new PurchaseRepo(context);

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

            string propertyToSort = GetPropertyName(sortBy, typeof(Product), "title");
  
            List<Product> products = _productRepo.GetAll(limit, propertyToSort, orderBy).ToList();

            return products;

        }

        private string GetPropertyName(string sortBy, Type type,string fallbackPropertyName = "")
        {
            PropertyInfo[] propInfos = type.GetProperties();
            string propertyName = null;
            foreach (PropertyInfo propertyInfo in propInfos)
            {
                if (propertyInfo.Name.ToUpper() == sortBy.ToUpper())
                {
                    propertyName = propertyInfo.Name;
                    break;
                }
            }
            if (!String.IsNullOrEmpty(fallbackPropertyName) && String.IsNullOrEmpty(propertyName))
            {
                propertyName =propInfos.FirstOrDefault(prop => prop.Name.ToUpper() == fallbackPropertyName.ToUpper()).Name;
            }
            if (String.IsNullOrEmpty(propertyName)) throw new Exception("Property not found");
            return propertyName;
        }

        public async Task<IEnumerable<Product>> Search(SearchQueryParameters queryParameters)
        {
            PropertyInfo propInfo = typeof(Product).GetProperty(queryParameters.SearchField,BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null) throw new Exception("Search Field Not Found");
            IEnumerable<Product> products = _productRepo.Search(propInfo.Name, queryParameters.SearchTerm);
            return products;
        }



        public async Task<IEnumerable<Product>> GetFromQuery(ProductQueryParameters queryParameters)
        {
            string propertyToSort = GetPropertyName(queryParameters.SortBy, typeof(Product), "title");
            IEnumerable<Product> products = await _productRepo.GetFromQuery(queryParameters);
            return products;
        }

        public void Update(int id, Product product)
        {
            var checkProduct =_productRepo.GetById(id);
            if (checkProduct == null) throw new Exception("Product Doesn't Exist");
            CopyAllNonIdValues(ref checkProduct, product);
            _productRepo.Update(checkProduct);
        }

        private void CopyAllNonIdValues(ref Product checkProduct, Product product)
        {
            PropertyInfo[] propInfos = typeof(Product).GetProperties();
            foreach (PropertyInfo propInfo in propInfos)
            {
                if (propInfo.Name == nameof(Product.ProductId)) continue;
                checkProduct.GetType().GetProperty(propInfo.Name).SetValue(checkProduct, propInfo.GetValue(product, null), null);
            }

        }

        public void Delete(int id)
        {
            Product productToDelete = _productRepo.GetById(id);
            if (productToDelete == null) throw new Exception("Product Not Found");
            _productRepo.Delete(productToDelete);
        }

        public IEnumerable<Product> DeleteRange(int[] ids)
        {
            List<Product> products = _productRepo.GetByIds(ids.ToList());
            _productRepo.DeleteRange(products);
            return products;
        }

        public List<Product> GetProductsFromUser(int userId)
        {
            List<int> productsIds = new List<int>();
            List<Purchase> purchases = _purchaseRepo.GetByUserId(userId).ToList();
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

        public void Create(Product newProduct){
            _productRepo.Create(newProduct);
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

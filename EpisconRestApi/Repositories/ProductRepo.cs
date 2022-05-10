using EpisconApi.DBContexts;
using EpisconApi.Helpers;
using EpisconApi.Models;
using System.Reflection;

namespace EpisconApi.Repositories
{
    public class ProductRepo
    {
        private readonly StoreContext _storeContext;

        public ProductRepo(StoreContext context)
        {
            _storeContext = context;
            _storeContext.Database.EnsureCreated();
        }

        public void UpdateFromRange(IEnumerable<Product> products)
        {
            if (products == null) throw new Exception("No Products Found");
            foreach (Product product in products)
            {
                bool exists = _storeContext.Products.Any(p => p.ProductId == product.ProductId);
                if (!exists)
                {
                    _storeContext.Products.Add(product);
                }
                else
                {
                    _storeContext.Products.Update(product);
                }
            }

            _storeContext.SaveChanges();
        }

        public Product GetById(int id)
        {
            Product product = _storeContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) throw new Exception("Product doesn't exist");
            return product;
        }

        public void Create(Product product)
        {
            if (product == null) throw new Exception("User to be created was null");
            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll(int limit, string sortBy, string orderBy)
        {
            var propertyInfo = sortBy != null ? typeof(Product).GetProperty(sortBy) : typeof(Product).GetProperty("Title");
            if (propertyInfo == null) throw new Exception("problem");
            if(orderBy.ToUpper() == "ASC")
            {
                return _storeContext.Products.AsEnumerable().OrderBy(x => propertyInfo.GetValue(x, null)).Take(limit);
            }
            else
            {
                return _storeContext.Products.AsEnumerable().OrderByDescending(x => propertyInfo.GetValue(x, null)).Take(limit);
            }
        }

        public async Task<IEnumerable<Product>> GetFromQuery(ProductQueryParameters queryParameters)
        {
            var propertyInfo = typeof(Product).GetProperty(queryParameters.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null 
                            ? typeof(Product).GetProperty(queryParameters.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) : 
                            typeof(Product).GetProperty("Title");
            IEnumerable<Product> products = null;
            if(queryParameters.MinPrice != null)
            {
                products = _storeContext.Products.AsEnumerable().Where(product => product.Price >= queryParameters.MinPrice);
            }
            if(queryParameters.MaxPrice != null)
            {
                if(products != null) products = products.Where(product => product.Price <= queryParameters.MaxPrice);
                else
                {
                   products = _storeContext.Products.AsEnumerable().Where(product => product.Price <= queryParameters.MaxPrice); 
                }
            }
            if(products == null)
            {
                if (queryParameters.OrderBy.ToUpper() == "ASC")
                {
                    products = _storeContext.Products.AsEnumerable().OrderBy(x => propertyInfo.GetValue(x, null)).Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
                }
                else
                {
                    products = _storeContext.Products.AsEnumerable().OrderByDescending(x => propertyInfo.GetValue(x, null)).Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
                }
            }
            else
            {
                if (queryParameters.OrderBy.ToUpper() == "ASC")
                {
                    products = products.OrderBy(x => propertyInfo.GetValue(x, null)).Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
                }
                else
                {
                    products = products.OrderByDescending(x => propertyInfo.GetValue(x, null)).Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
                }
            }
            return products;
   
        }

        public IEnumerable<Product> Search(string fieldName, string searchTerm)
        {
            IEnumerable<Product> products = _storeContext.Products.AsEnumerable().Where(product => Convert.ToString(product.GetType().GetProperty(fieldName).GetValue(product, null)).Contains(searchTerm,StringComparison.OrdinalIgnoreCase));
            return products;
        }

        public List<Product> GetByIds(List<int> ids)
        {
            List<Product> products = new List<Product>();
            foreach (int id in ids)
            {
                products.Add(GetById(id));
            }
            return products;
        }
    }
}

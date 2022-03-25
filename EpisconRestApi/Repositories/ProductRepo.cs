using EpisconApi.DBContexts;
using EpisconApi.Models;

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
            var propertyInfo = typeof(Product).GetProperty(sortBy);
            if(orderBy.ToUpper() == "ASC")
            {
                return _storeContext.Products.AsEnumerable().OrderBy(x => propertyInfo.GetValue(x, null)).Take(limit);
            }
            else
            {
                return _storeContext.Products.AsEnumerable().OrderByDescending(x => propertyInfo.GetValue(x, null)).Take(limit);
            }
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

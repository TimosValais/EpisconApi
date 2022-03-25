using EpisconApi.DBContexts;
using EpisconApi.Models;

namespace EpisconApi.Repositories
{
    public class PurchaseRepo
    {
        private readonly StoreContext _storeContext;

        public PurchaseRepo(StoreContext context)
        {
            _storeContext = context;
            _storeContext.Database.EnsureCreated();
        }

        public void Create(Purchase purchase)
        {
            if (purchase == null) throw new Exception("User to be created was null");
            _storeContext.Purchases.Add(purchase);
            _storeContext.SaveChanges();
        }

        public IEnumerable<Purchase> GetByUserId(int userId)
        {
            return _storeContext.Purchases.Where(p => p.UserId == userId);
        }
        public IEnumerable<Purchase> GetByProductId(int product)
        {
            return _storeContext.Purchases.Where(p => p.ProductId == product);
        }
    }
}

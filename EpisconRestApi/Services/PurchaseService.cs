using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Repositories;

namespace EpisconApi.Services
{
    public class PurchaseService
    {
        private UserService _userService;
        private ProductService _productService;
        private PurchaseRepo _purchasetRepo;
        public PurchaseService(StoreContext context)
        {
            _purchasetRepo = new PurchaseRepo(context);
            _userService = new UserService(context);
            _productService = new ProductService(context);
        }

        public void New(int productId, int userId)
        {
            User user = _userService.GetById(userId);
            Product product = _productService.GetById(productId);
            if (product == null || user == null) throw new Exception("Data not found");
            Purchase purchase = new Purchase
            {
                ProductId = productId,
                UserId = userId,
                Product = product,
                User = user,
                PurchaseDate = DateTime.Now
            };
            _purchasetRepo.Create(purchase);
        }

        public IEnumerable<Purchase> GetByUser(int userId)
        {
            var purchases = _purchasetRepo.GetByUserId(userId);
            if (purchases == null) throw new Exception("No users found");
            return purchases;
        }

        public IEnumerable<Purchase> GetByProduct(int productId)
        {
            var purchases = _purchasetRepo.GetByProductId(productId);
            if (purchases == null) throw new Exception("No users found");
            return purchases;
        }
    }
}

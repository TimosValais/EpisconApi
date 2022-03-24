using EpisconApi.DBContexts;
using EpisconApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EpisconApi.Repositories
{
    public class UserRepo
    {
        private readonly StoreContext _storeContext;

        public UserRepo(StoreContext context)
        {
            _storeContext = context;
            _storeContext.Database.EnsureCreated();
        }
        public void AddOrUpdate(User user)
        {

            bool exists = _storeContext.Users.Any(u => u.UserId == user.UserId);
            if (!exists)
            {
                _storeContext.Users.Add(user);
            }
            else
            {
                _storeContext.Users.Update(user);
            }
            _storeContext.SaveChanges();
        }
        public IEnumerable<User> GetAll()
        {
            return _storeContext.Users.ToArray();
        }
        public User GetById(int id)
        {
            return _storeContext.Users.FirstOrDefault(u => u.UserId == id);
        }

    }
}

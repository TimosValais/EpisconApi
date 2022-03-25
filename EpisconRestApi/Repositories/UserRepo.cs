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
                this.Create(user);
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
        public void Create(User user)
        {
            if (user == null) throw new Exception("User to be created was null");
            _storeContext.Users.Add(user);
            _storeContext.SaveChanges();
        }

        public void Delete(int id)
        {
            User userToDelete = this.GetById(id);
            if (userToDelete == null) throw new Exception("User to be deleted was not found");
            _storeContext.Users.Remove(userToDelete);
            _storeContext.SaveChanges();
        }
    }
}

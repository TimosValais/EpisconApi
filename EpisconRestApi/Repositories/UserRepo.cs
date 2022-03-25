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
            User user = _storeContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) throw new Exception("User doesn't exist");
            return user;
        }
        public void Create(User user)
        {
            if (user == null) throw new Exception("User to be created was null");
            _storeContext.Users.Add(user);
            _storeContext.SaveChanges();
        }

        public void AddOrUpdateRange(IEnumerable<User> users)
        {
            if (users == null) throw new Exception("No Products Found");

            foreach (User user in users)
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
            }

            _storeContext.SaveChanges();
        }

        public void Delete(int id)
        {
            User userToDelete = this.GetById(id);
            if (userToDelete == null) throw new Exception("User to be deleted was not found");
            _storeContext.Users.Remove(userToDelete);
            _storeContext.SaveChanges();
        }
        public List<User> GetByIds(List<int> ids)
        {
            List<User> users = new List<User>();
            foreach (int id in ids)
            {
                users.Add(GetById(id));
            }
            return users;
        }
    }
}

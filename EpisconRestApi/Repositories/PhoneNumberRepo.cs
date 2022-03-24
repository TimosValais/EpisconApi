using EpisconApi.DBContexts;
using EpisconApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EpisconApi.Repositories
{
    public class PhoneNumberRepo
    {
        private readonly StoreContext _storeContext;

        public PhoneNumberRepo(StoreContext context)
        {
            _storeContext = context;
            _storeContext.Database.EnsureCreated();
        }
        public void AddOrUpdate(PhoneNumber phoneNumber)
        {

            bool exists = _storeContext.PhoneNumbers.Any(u => u.PhoneNumberId == phoneNumber.PhoneNumberId);
            if (!exists)
            {
                _storeContext.PhoneNumbers.Add(phoneNumber);
            }
            else
            {
                _storeContext.PhoneNumbers.Update(phoneNumber);
            }
            _storeContext.SaveChanges();
        }

        public IEnumerable<PhoneNumber> GetByUserId(int userId)
        {
            return _storeContext.PhoneNumbers.Where(u => u.UserId == userId);
        }
    }
}

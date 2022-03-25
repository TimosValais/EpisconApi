using EpisconApi.DBContexts;
using EpisconApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EpisconApi.Repositories
{
    public class AddressRepo
    {
        private readonly StoreContext _storeContext;

        public AddressRepo(StoreContext context)
        {
            _storeContext = context;
            _storeContext.Database.EnsureCreated();
        }
        public void AddOrUpdate(Address address)
        {

            bool exists = _storeContext.Addresses.Any(u => u.AddressId == address.AddressId);
            if (!exists)
            {
                _storeContext.Addresses.Add(address);
            }
            else
            {
                _storeContext.Addresses.Update(address);
            }
            _storeContext.SaveChanges();
        }

        public void AddOrUpdateRange(IEnumerable<Address> addresses)
        {
            if (addresses == null) throw new Exception("No Products Found");

            foreach (Address address in addresses)
            {
                bool exists = _storeContext.Addresses.Any(u => u.AddressId == address.AddressId);
                if (!exists)
                {
                    _storeContext.Addresses.Add(address);
                }
                else
                {
                    _storeContext.Addresses.Update(address);
                }
            }
            _storeContext.SaveChanges();
        }


        public Address GetById(int id)
        {
            return _storeContext.Addresses.FirstOrDefault(addr => addr.AddressId == id);
        }

        public IEnumerable<Address> GetAll()
        {
            return _storeContext.Addresses.ToArray();
        }

        public Address GetByFields(Address? address)
        {
            return _storeContext.Addresses.FirstOrDefault(add => add.StreetAddress == address.StreetAddress && add.City == address.City && add.State == address.State);
        }
    }
}


using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Repositories;

namespace EpisconApi.Services
{
    public class UserService
    {
        private AddressRepo _addressRepo;
        private UserRepo _userRepo;
        private PhoneNumberRepo _phoneNumberRepo;

        public UserService(StoreContext context)
        {
            _addressRepo = new AddressRepo(context);
            _userRepo = new UserRepo(context);
            _phoneNumberRepo = new PhoneNumberRepo(context);
        }

        public void AddOrUpdateRange(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                HandleSameAddress(user);
                _addressRepo.AddOrUpdate(user.Address);
                foreach (PhoneNumber phoneNumber in user.PhoneNumbers)
                {
                    _phoneNumberRepo.AddOrUpdate(phoneNumber);
                }
                //_storeContext.SaveChanges();

                _userRepo.AddOrUpdate(user);
            }

        }

        private void HandleSameAddress(User user)
        {
            Address checkAddress = _addressRepo.GetByFields(user.Address);
            if (checkAddress != null)
            {
                user.AddressId = checkAddress.AddressId;
                user.Address = checkAddress;
            }
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _addressRepo.GetAll();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _userRepo.GetAll();
            foreach (User user in users)
            {
                user.Address = _addressRepo.GetById(user.AddressId);
                user.PhoneNumbers = _phoneNumberRepo.GetByUserId(user.UserId).ToList();
            }
            return users;
        }
    }
}

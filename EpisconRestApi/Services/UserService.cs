using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Repositories;
using Newtonsoft.Json;

namespace EpisconApi.Services
{
    public class UserService
    {
        private AddressRepo _addressRepo;
        private UserRepo _userRepo;
        private PhoneNumberRepo _phoneNumberRepo;
        private PurchaseRepo _purchaseRepo;

        public UserService(StoreContext context)
        {
            _addressRepo = new AddressRepo(context);
            _userRepo = new UserRepo(context);
            _phoneNumberRepo = new PhoneNumberRepo(context);
            _purchaseRepo = new PurchaseRepo(context);
        }

        public void Create(User user)
        {
            _userRepo.Create(user);
        }
        public void AddOrUpdateRange(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                HandleSameAddress(user);
                foreach (PhoneNumber phoneNumber in user.PhoneNumbers)
                {
                    _phoneNumberRepo.AddOrUpdate(phoneNumber);
                }
            }
            var addresses = users.Select(x => x.Address);
            _addressRepo.AddOrUpdateRange(addresses);
            _userRepo.AddOrUpdateRange(users);


        }

        public List<User> GetByIds(List<int> ids)
        {
            return _userRepo.GetByIds(ids);
        }

        public IEnumerable<User> GetSampleDataFromJson()
        {
            string jsonText = @"
            [
                {
                    'userId': '1',
                    'firstName': 'Joe',
                    'lastName': 'Jackson',
                    'gender': 'male',
                    'age': 28,
                    'address': {
                        'streetAddress': '101',
                        'city': 'San Diego',
                        'state': 'CA'
                    },
                    'phoneNumbers': [
                        {'type': 'home', 'number': '7349282382' }
                    ]
                },
                {
                    'userId': '2',
                    'firstName': 'William',
                    'lastName': 'Franklin',
                    'gender': 'female',
                    'age': 34,
                    'address': {
                        'streetAddress': '967',
                        'city': 'Texas',
                        'state': 'Texas'
                    },
                    'phoneNumbers': [
                        {'type': 'mobile', 'number': '2342347097' }
                    ]
                }
            ]";

            try
            {
                IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonText);
                return users;
            }
            catch (Exception ex)
            {

                throw new Exception("Serialize failed, data not in correct format");
            }

        }

        public List<User> GetUsersFromProduct(int productId)
        {
            List<int> userIds = new List<int>();
            List<Purchase> purchases = _purchaseRepo.GetByProductId(productId).ToList();
            foreach (var purchase in purchases)
            {
                if(!userIds.Any(userId => userId == purchase.UserId))
                {
                    userIds.Add(purchase.UserId);
                }
            }
            return GetByIds(userIds);
        }

        public User GetById(int id)
        {
            return _userRepo.GetById(id);
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

        public void Delete(int id)
        {
            _userRepo.Delete(id);
        }
    }
}

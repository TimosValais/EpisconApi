using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using Microsoft.EntityFrameworkCore;
using EpisconApi.Models;

namespace EpisconApi.DBContexts
{
    public static class BuilderExtend
    {
        public static void Seed(this ModelBuilder builder)
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
                        { 'type': 'home', 'number': '7349282382' }
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
                        { 'type': 'mobile', 'number': '2342347097' }
                    ]
                }
            ]";

            List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonText);

            foreach (User user in users)
            {
                builder.Entity<Address>().HasData(new Address
                {
                    City = user.Address.City,
                    State = user.Address.State,
                    StreetAddress = user.Address.StreetAddress
                });
                foreach (PhoneNumber phoneNumber in user.PhoneNumbers)
                {
                    builder.Entity<PhoneNumber>().HasData(new PhoneNumber
                    {
                        Number = phoneNumber.Number,
                        Type = phoneNumber.Type
                    });
                }
                builder.Entity<User>().HasData(new User { 
                    UserId = user.UserId,
                    Address = user.Address, 
                    Age = user.Age, 
                    FirstName = user.FirstName, 
                    Gender = user.Gender, 
                    LastName = user.LastName,
                    PhoneNumbers = user.PhoneNumbers});
            }
        }
    }
}

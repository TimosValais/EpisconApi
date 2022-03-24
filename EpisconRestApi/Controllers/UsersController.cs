﻿using EpisconApi.DBContexts;
using EpisconApi.Models;
using EpisconApi.Repositories;
using EpisconApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace EpisconApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private UserService _userService;

        public UsersController(StoreContext context)
        {
            _userService = new UserService(context);
        }
        [HttpGet]
        public object GetAll()
        {
            var users = _userService.GetAll().ToList();
            return JsonConvert.SerializeObject(users);
        }
        [HttpPut]
        public HttpResponseMessage Update()
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
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonText);
                _userService.AddOrUpdateRange(users);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
           
        }
    }
}
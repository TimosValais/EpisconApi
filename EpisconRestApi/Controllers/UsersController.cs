using EpisconApi.DBContexts;
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
        [Route("UpdateFromSampleJson")]
        [HttpPut]
        public HttpResponseMessage UpdateFromSampleJson()
        {

            try
            {
                List<User> users = _userService.GetSampleDataFromJson().ToList();
                _userService.AddOrUpdateRange(users);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        public HttpResponseMessage Create(object user)
        {
            try
            {
                User userToCreate = JsonConvert.DeserializeObject<User>(Convert.ToString(user));
                _userService.Create(userToCreate);
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            }
        }
        [Route("Get/{id}")]
        [HttpGet]
        public object Get(int id)
        {
            try
            {
                User user = _userService.GetById(id);
                return JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        [Route("GetUsersFromProduct/{productId}")]
        [HttpGet]
        public object GetUsersFromProduct(int productId)
        {
            try
            {
                List<User> users = _userService.GetUsersFromProduct(productId);
                return users;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        [Route("Delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flespi.Core.AppService;
using Flespi.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flespi.REST.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        // GET /<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        // POST /<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User newUser)
        {
            try
            {
                return Ok(_userService.CreateUser(newUser));
            }
            catch (Exception e)
            {
                return BadRequest("cannot create User sorry!" + e.Message);
            }
            
        }

        // PUT /<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            if (id< 1 || id!= user.Id)
            {
                return BadRequest("parameter id and user.id must be the same");
            }

            return Ok(_userService.UpdateUser(user));
        }

        // DELETE /<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var user = _userService.DeleteUser(id);
            if (user == null)
            {
                return StatusCode(404, "Did not find any user with id " + id);
            }

            return NoContent();
        }
    }
}

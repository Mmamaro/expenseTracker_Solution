using expenseTracker_Api.Models;
using expenseTracker_Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace expenseTracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly usersRepository _usersRepository;

        public usersController(usersRepository usersRepository)
        {
           _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<User> user = await _usersRepository.GetAllUsersAsync();

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            User? user = await _usersRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] User userObj)
        {
            if (userObj == null || userObj.name == "string" || userObj.surname == "string" 
                || userObj.email == "string" || userObj.salary <= 0)
            {
                return BadRequest();
            }

            await _usersRepository.AddUserAsync(userObj);

            return Ok("User Added successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] User userObj)
        {
            if (userObj == null || userObj.name == "string" || userObj.surname == "string" 
                || userObj.email == "string" || userObj.salary <= 0)
            {
                return BadRequest();
            }

            await _usersRepository.UpdateUserAsync(userObj);
            return Ok("User Updated successfully");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {

                if (Id == 0)
                {
                    return NotFound();
                }

            await _usersRepository.DeleteUserAsync(Id);

            return Ok("User deleted successfully");

        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchUser(string? email, string? name) // search by any column name
        {

            var user = await _usersRepository.SearchUserAsync(email, name);

            return Ok(user);
        }
    }
}

using DataAccess.Repository.Implements;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        private IUserRepository repository = new UserRepository();
        private ITokenRepository tokenRepository;
        public UsersController(ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }


        [HttpPost("SignIn")]
        public IActionResult SignIn(SignInModel model)
        {
            try
            {
                var user = repository.Login(model);
                if (user != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim("Id", user.user_id.ToString()),
                        new Claim("Role", user.Role.roleName),
                        new Claim("Name", user.fullName),
                        new Claim("Thumb", user.thumb),
                        new Claim(ClaimTypes.Role, user.Role.roleName)
                    };
                    var token = tokenRepository.GetToken(authClaims);
                    return Ok(new LoginResponse
                    {
                        Token = token.TokenString,
                        Expiration = token.ValidTo,
                        StatusCode = 1,
                        Message = "Logged in successfullly"
                    });
                }
                // Login failed
                return Ok(new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid email or password"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get All 
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAllUser());
        }
        [HttpPost("SignUp")]
        public IActionResult SignUp(SignUpModel model)
        {
            try
            {
                repository.Add(model);
                return Ok("Sucessfully registered!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [EnableQuery]
        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingUser = repository.GetUsersById(key);
                if (existingUser == null)
                    return NotFound("User not found.");

                existingUser.user_id = key;
                existingUser.thumb = userDTO.thumb;
                existingUser.fullName = userDTO.fullName;
                existingUser.phone = userDTO.phone;

                repository.Update(existingUser);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Delete
        [EnableQuery]
        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            try
            {
                var existingUser = repository.GetUsersById(key);
                if (existingUser == null)
                    return NotFound("User not found.");

                repository.Delete(key);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("You have successfully logged out");
        }

    }
}

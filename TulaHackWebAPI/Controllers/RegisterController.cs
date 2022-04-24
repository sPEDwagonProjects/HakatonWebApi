using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TulaHackWebAPI.Context;
using TulaHackWebAPI.Model;
using TulaHackWebAPI.Utils;

namespace TulaHackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
       
        [HttpGet("/Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
                using (UserDBAppContext dB = new UserDBAppContext())
                {
                    if (await dB.UserExist(username))
                    {
                        return BadRequest(new Exceptions.UserExist());
                    }

                    User newuser = new User()
                    {
                        Login = username,
                        Password = Generators.GetMD5(password),
                        RoleId = 0,
                    };

                   await dB.AddAsync(newuser);
                   await dB.SaveChangesAsync();
                   
                }
                return Ok();
        }

    }
}
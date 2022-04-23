using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TulaHackWebAPI.Context;
using TulaHackWebAPI.Etc;
using TulaHackWebAPI.Model;
using TulaHackWebAPI.Utils;

namespace TulaHackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController : ControllerBase
    {
        [Authorize]
        public async Task<IActionResult> AuthCheck()
        {
            return await  Task.Run(() =>
            {
                return Ok();
            });
        }
        [HttpGet("/Auth")]
        public async Task<IActionResult> Auth(string username, string password)
        {
            var user = await GetUserFormDB(username, password);
            
            //Если пользователь не найден
            if (user == null)
            {
                return Unauthorized(new Exceptions.InvalidUserNameOrPassword());
            }

            var token = Token.GenerateToken(user);
           
            //Формеруем ответ
            var response = new
            {
                username=token.user_name,
                access_token = token.access_token,
                role = token.roleid,
                experiense_in = token.expires_in,
            };
            //Отправляем
            return Ok(response);
        }

      
        private async Task<User?> GetUserFormDB(string username,string password)
        {
            try
            {
                using (UserDBAppContext userDBAppContext = new UserDBAppContext())
                {
                    return await userDBAppContext.GetUserAsync(username, Generators.GetMD5(password));
                }
            }
            catch(Exception) { return null; }
        }

        
    }
}

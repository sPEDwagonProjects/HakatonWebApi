using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace TulaHackWebAPI.Utils
{
    public static class Generators
    { 
        public static string GetMD5(string data)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}

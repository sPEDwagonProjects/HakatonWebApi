using Microsoft.EntityFrameworkCore;
using TulaHackWebAPI.Model;

namespace TulaHackWebAPI.Context
{
    public class UserDBAppContext:DBContextBase
    {
        
        public DbSet<User> Users { get; set; } = null!;
        public async Task<User> GetUserAsync(string login, string password)
        {
            return await Users.FirstAsync(u => u.Login == login && u.Password == password);
        }
        public async Task<bool> UserExist(string login)
        {
            Console.WriteLine(Users.Count());
            return await Users.Where(x => x.Login == login).CountAsync() > 0;    
        }
        public async Task<User> GetUserByLogin(string login)
        {
             return await Users.FirstOrDefaultAsync(x => x.Login == login);
        }

       
       
    }

}

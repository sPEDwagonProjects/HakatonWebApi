using Microsoft.EntityFrameworkCore;

namespace TulaHackWebAPI.Context
{
    public class DBContextBase:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=remotemysql.com;user=TJo9rkR2RE;password=TpeduUH8Hz;database=TJo9rkR2RE;",
                new MySqlServerVersion(new Version(8, 0, 13))
                );
        }
    }
}

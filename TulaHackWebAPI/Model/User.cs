using System.ComponentModel.DataAnnotations.Schema;

namespace TulaHackWebAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;


namespace GlobalNewsAPI.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserRegisterDate { get; set; }
    }
}

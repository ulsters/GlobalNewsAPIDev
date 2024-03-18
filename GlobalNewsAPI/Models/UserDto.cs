using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalNewsAPI.Models
{
    public class UserDto
    {
        [Key]
        public int UserDtoId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }

    }
}

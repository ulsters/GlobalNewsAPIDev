using System.ComponentModel.DataAnnotations;

namespace GlobalNewsAPI.Models
{
    public class UserComments
    {
        [Key]
        public int UserCommentId { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }
        public string UserComment { get; set; }
        public DateTime UserCommentDate { get; set; }
    }
}

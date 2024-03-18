using System.ComponentModel.DataAnnotations;

namespace GlobalNewsAPI.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime NewsPublishDate { get; set; }
        public string NewsPopularityType { get; set; }
        public int CountryId { get; set; }
        public string? NewsImageUrl { get; set; }
    }
}

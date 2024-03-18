namespace GlobalNewsAPI.Models
{
    public class PopularNews
    {
        public int PopularNewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime NewsPublishDate { get; set; }
        public string NewsPopularityType { get; set; }
        public int CountryId { get; set; }
    }
}

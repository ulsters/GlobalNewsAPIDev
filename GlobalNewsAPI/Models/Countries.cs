using System.ComponentModel.DataAnnotations;

namespace GlobalNewsAPI.Models
{
    public class Countries
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}

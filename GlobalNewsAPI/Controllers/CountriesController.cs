using GlobalNewsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GlobalNewsAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
         
        private readonly RepositoryContext _context;
        private readonly ILogger<NewsController> _logger;

        public CountriesController(RepositoryContext context, ILogger<NewsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]

        public IActionResult GetCountries()
        {

            try
            {
                var countries = _context.Countries.ToList();
                return Ok(countries);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{countryName}")]
        public IActionResult GetNewsByCountry(string countryName)
        {
            try
            {
                // Find the country by name
                var country = _context.Countries.FirstOrDefault(c => c.CountryName == countryName);

                if (country == null)
                {
                    _logger.LogWarning("Country not found: {CountryName}", countryName);
                    return NotFound("Country not found"); // Return 404 if the country is not found
                }

                // Retrieve news associated with the found country
                var news = _context.News
                    .Where(n => n.CountryId == country.CountryId)
                    .ToList();

                if (news.Count == 0)
                {
                    _logger.LogInformation("No news found for country: {CountryName}", countryName);
                    return NotFound("No news found for this country"); // Return 404 if no news is found for the country
                }

                return Ok(news); // Return the retrieved news items
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing request for country: {CountryName}", countryName);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }


}


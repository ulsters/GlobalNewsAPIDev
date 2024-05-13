using System.Data;
using System.Diagnostics.Eventing.Reader;
using GlobalNewsAPI.Models;
using GlobalNewsAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NewsController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public NewsController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllNews()
        {
            try
            {
                var news = _context.News.ToList();
                return Ok(news);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNews([FromRoute(Name = "id")] int id)
        {
            try
            {
                var news = _context.News.SingleOrDefault(n => n.NewsId == id);

                if (news is null)
                {
                    return NotFound(); // 404
                }

                return Ok(news); // Return the retrieved news item

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }


        [HttpPost]

        public IActionResult CreateOneNews([FromBody] News news)
        {
            try
            {
                if (news is null)
                {
                    return BadRequest();
                }

                _context.News.Add(news);
                _context.SaveChanges();

                return StatusCode(201, news);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        [HttpPut("{id:int}")]

        public IActionResult UpdateOneNews([FromRoute(Name = "id")] int id, [FromBody] News news)
        {
            try
            {
                //check news
                var entity = _context.News.Where(b => b.NewsId.Equals(id)).SingleOrDefault();

                if (entity is null)
                {
                    return NotFound(); // 404
                }
                //check news id
                if (id != news.NewsId)
                {
                    return BadRequest(); // 400
                }

                entity.NewsTitle = news.NewsTitle;
                entity.NewsContent = news.NewsContent;
                entity.NewsPopularityType = news.NewsPopularityType;
                entity.NewsPublishDate = news.NewsPublishDate;
                entity.NewsImageUrl = news.NewsImageUrl;

                _context.SaveChanges();

                return Ok(news);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteNews([FromRoute(Name = "id")] int id)
        {

            try
            {
                var entity = _context.News.Where(n => n.NewsId.Equals(id)).SingleOrDefault();

                if (entity is null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} could not found."
                    }); //404
                }

                _context.News.Remove(entity);
                _context.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }





}
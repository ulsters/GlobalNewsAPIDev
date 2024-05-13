using Microsoft.AspNetCore.Mvc;

namespace GlobalNewsAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

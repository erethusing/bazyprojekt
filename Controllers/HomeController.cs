using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
   
    public class HomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View(); // Upewnij siê, ¿e masz widok Welcome.cshtml
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

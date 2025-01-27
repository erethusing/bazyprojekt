using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // 🔒 Tylko dla zalogowanych
[ApiController]
[Route("api/[controller]")]  // Dodanie trasy do całego kontrolera
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MaisAbrigo.Controllers
{
    public class AbrigoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

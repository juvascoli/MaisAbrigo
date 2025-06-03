using Microsoft.AspNetCore.Mvc;

namespace MaisAbrigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

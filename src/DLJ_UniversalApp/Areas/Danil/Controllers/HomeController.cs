using Microsoft.AspNetCore.Mvc;

namespace DLJ_UniversalApp.Areas.Danil.Controllers
{
    [Area("danil")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

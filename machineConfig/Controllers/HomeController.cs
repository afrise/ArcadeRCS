using Microsoft.AspNetCore.Mvc;

namespace machineConfig.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
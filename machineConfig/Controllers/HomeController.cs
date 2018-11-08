using machineConfig.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace machineConfig.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        [HttpPost("GetGames")]    public ActionResult GetGames() => new JsonResult(new { Data = GamesRepository.Instance.Games }); 
        [HttpPost("Reboot")]      public void         Reboot() => Process.Start("shutdown.exe", "-r -t 0");
        [HttpPost("AddGame")]     public void         AddGame(string name, string command) => GamesRepository.Instance.AddGame(name, command);
        [HttpPost("SetGame")]     public void         SetGame(string name) => GamesRepository.Instance.CurrentGame=name; 
        [HttpPost("Delete")]      public void         Delete(string name) => GamesRepository.Instance.RemoveGame(name);
        [HttpPost("SoftReboot")]  public void         SoftReboot() => GameProcessService.Instance.Start(GamesRepository.Instance.CurrentPath);
    }
}
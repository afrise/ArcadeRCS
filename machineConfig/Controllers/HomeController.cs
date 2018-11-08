using machineConfig.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace machineConfig.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        [HttpPost("GetGames")] public ActionResult GetGames() => new JsonResult(new { Data = GamesRepository.Games }); 
        [HttpPost("Reboot")] public void Reboot() => Process.Start("shutdown.exe", "-r -t 0");
        [HttpPost("AddGame")] public void AddGame(string name, string command) => GamesRepository.AddGame(name, command);
        [HttpPost("SetGame")] public void SetGame(string name) => GamesRepository.CurrentGame=name; 
        [HttpPost("Delete")] public void Delete(string name) => GamesRepository.RemoveGame(name);
        [HttpPost("SoftReboot")]
        public void SoftReboot()
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = GamesRepository.Games.Where(g => g.GameName ==GamesRepository.CurrentGame).First().Path;
                p.Start();
                Console.WriteLine("Game Process Started.");
            }
            catch { Console.WriteLine("Error spawning game process, sorry kiddo."); }
        }
    }
}

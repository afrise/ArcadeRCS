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
        [HttpPost("GetGames")] public ActionResult GetGames() => new JsonResult(new { Data = GamesRepository.Instance.Games }); 
        [HttpPost("Reboot")] public void Reboot() => Process.Start("shutdown.exe", "-r -t 0");
        [HttpPost("AddGame")] public void AddGame(string name, string command) => GamesRepository.Instance.AddGame(name, command);
        [HttpPost("SetGame")] public void SetGame(string name) => GamesRepository.Instance.CurrentGame=name; 
        [HttpPost("Delete")] public void Delete(string name) => GamesRepository.Instance.RemoveGame(name);
        [HttpPost("SoftReboot")]
        public void SoftReboot()
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = GamesRepository.Instance.Games
                    .Where(g => g.GameName == GamesRepository.Instance.CurrentGame).First().Path;
                p.Start();
                Console.WriteLine("Game Process Started.");
            }
            catch { Console.WriteLine("Error spawning game process, sorry kiddo."); }
        }
    }
}

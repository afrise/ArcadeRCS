using machineConfig.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace machineConfig
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //spin up a process for the selected game. should be elsewhere but for now this works
            //GamesRepository.Instance.WipeAndRefresh(); //DEBUG
            var games = GamesRepository.Instance.Games;
            var game = new Game();
            if (!games.Any(g => g.Selected)) game = games.First();
            else game = games.Single(g => g.Selected);
            Console.WriteLine($"Default Game: {game.GameName}. Loading...");
            var p = new Process();
            p.StartInfo.FileName = game.Path;
            p.Start();

            
            new WebHostBuilder()
                .UseUrls("http://0.0.0.0")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
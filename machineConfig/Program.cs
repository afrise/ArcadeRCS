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
            GameProcessService.Instance.Start(GamesRepository.Instance.CurrentPath);

            //

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
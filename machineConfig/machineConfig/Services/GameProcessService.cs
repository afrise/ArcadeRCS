using System;
using System.Diagnostics;

namespace machineConfig.Services
{
    public sealed class GameProcessService
    {
        private GameProcessService() { }
        private static readonly Lazy<GameProcessService> lazy = new Lazy<GameProcessService>(() => new GameProcessService());
        public static GameProcessService Instance => lazy.Value;
        private Process GameProcess;
        private readonly string Path="ECHO";

        public void Start() => Start(Path);
        public void Start(string path)
        {
            try
            {
                if (GameProcess == null || GameProcess.HasExited)
                {
                    GameProcess = new Process();
                    GameProcess.StartInfo.FileName = path;
                    GameProcess.Start();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message); // show why it didn't boot the game on the console and bring up normally
            }
        }

        public void Restart() { Stop(); Start(); }

        public void Stop() { GameProcess.Close(); GameProcess.Dispose(); }
    }
}
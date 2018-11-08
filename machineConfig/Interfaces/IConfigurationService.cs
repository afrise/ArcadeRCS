using machineConfig.Services;
using System.Collections.Generic;

namespace machineConfig.Interfaces
{
    public interface IConfigurationService
    {
        string CurrentGame { get; set; }
        List<Game> Games { get; }
        IConfigurationService SetCurrentGame(string name);
        IConfigurationService RemoveGame(string name);
        IConfigurationService AddGame(string name, string path);
    }
}

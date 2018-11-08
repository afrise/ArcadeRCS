using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace machineConfig.Services
{
    public sealed class GamesRepository 
    {
        private static readonly Lazy<GamesRepository> lazy = new Lazy<GamesRepository>(() => new GamesRepository());
        public static GamesRepository Instance => lazy.Value;
        private GamesRepository() { }
        private SQLiteConnection Connection => new SQLiteConnection("Data Source=data/games.db;Version=3;");

        public string CurrentGame
        {
            get => Connection.QuerySingle<string>("SELECT * FROM Games WHERE Selected=1");
                        
            set => Connection.Execute("UPDATE Games SET Selected=0;UPDATE Games SET Selected=1 WHERE GameName=@name", 
                new { name = value }); 
        }

        public List<Game> Games => 
            Connection.Query<Game>("SELECT * FROM Games").ToList();

        public void WipeAndRefresh() =>
            Connection.Execute("DROP TABLE Games" + 
                               "CREATE TABLE Games (GameName VARCHAR(200), Path VARCHAR(400), Selected BIT)" +
                               "INSERT INTO Games VALUES('Default(Microsoft Paint)', 'mspaint.exe', 1)");
        
        public void AddGame(string name, string path) => 
            Connection.Execute("INSERT INTO Games VALUES (@name, @path, 0)", 
                                new { name, path });

        public void RemoveGame(string name) => 
            Connection.Execute("DELETE FROM Games WHERE GameName=@name", 
                                new { name }); 
    }
    
    public class Game
    {
        public Game(string name, string path, bool selected=false) { GameName = name; Path = path;  Selected = selected; }
        public Game() { GameName = "New Game"; Path = "mspaint.exe"; Selected = false; }
        public string GameName { get; set; }
        public string Path { get; set; }
        public bool Selected { get; set; }
    }
}

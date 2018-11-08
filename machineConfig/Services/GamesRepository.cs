using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace machineConfig.Services
{
    public static class GamesRepository 
    {
        private static SQLiteConnection connection => new SQLiteConnection("Data Source=data/games.db;Version=3;");
        public static string CurrentGame
        {
            get{
                using (var db = connection) while (true) try {return db.QuerySingle<string>("SELECT * FROM Games WHERE Selected=1");}
                        catch (Exception e){Console.WriteLine(e.Message);}}
            set{
                var success = false;
                using (var db = connection) while (!success) try
                        { db.Execute("UPDATE Games SET Selected=0;UPDATE Games SET Selected=1 WHERE GameName=@name", new { name = value }); success=true; }
                        catch(Exception e) {Console.WriteLine(e.Message);}}
        }

        public static List<Game> Games
        {
            get
            {
                using (var db = connection) while (true) try
                        { return db.Query<Game>("SELECT * FROM Games").ToList(); }
                        catch(Exception e){ Console.WriteLine(e.Message); }
            }
        }

        public static void WipeAndRefresh(){ //not calling this rn
            using (var db = connection) {
                 db.Execute("DROP TABLE Games");
                 db.Execute("CREATE TABLE Games (GameName VARCHAR(200), Path VARCHAR(400), Selected BIT)");
                 db.Execute("INSERT INTO Games VALUES('Default(Microsoft Paint)', 'mspaint.exe', 1)");
            }
        }//if it doesn't work, oh well.

        public static void AddGame(string name, string path) {
            var success = false;
            while (!success) using (var db = connection) try { db.Execute("INSERT INTO Games VALUES (@name, @path, 0)", new { name, path }); success=true; }
                    catch(Exception e) { Console.WriteLine(e.Message); } }

        public static void RemoveGame(string name) {
            var success = false;
            while (!success) using (var db = connection) try { db.Execute("DELETE FROM Games WHERE GameName=@name", new { name });success=true; }
                    catch (Exception e){ Console.WriteLine(e.Message); };
        }
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

using System.Collections.Generic;
using LiteDB;

namespace Notori
{
    public static class DbManager
    {
        private static readonly string connectionString = "Notori.db";

        public static List<Note> GetNotes()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var notes = db.GetCollection<Note>("notes");
                return notes.Query().Select(x => x).ToList();
            }
        }

        public static List<Note> GetTodos()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var todos = db.GetCollection<Note>("todos");
                return todos.Query().Select(x => x).ToList();
            }
        }

        public static void AddNote(Note note)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var notes = db.GetCollection<Note>("notes");
                notes.Insert(note);
            }
        }

        public static void AddTodo(Note note)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var todos = db.GetCollection<Note>("todos");
                todos.Insert(note);
            }
        }

        public static void UpdateNote(Note note)
        {

        }

        public static void UpdateTodo(Note todo)
        {

        }

        public static void DeleteNote(int id)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var bsonId = new LiteDB.BsonValue(id);
                var notes = db.GetCollection<Note>("notes");
                notes.Delete(bsonId);
            }
        }

        public static void DeleteTodo(int id)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var todos = db.GetCollection<Note>("todos");
                var bsonId = new LiteDB.BsonValue(id);
                todos.Delete(bsonId);
            }
        }

        public static void DropNotes()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                db.DropCollection("notes");
            }
        }

        public static void DropTodos()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                db.DropCollection("todos");
            }
        }

        public static Settings GetSettings()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var settings = db.GetCollection<Settings>("settings");
                var setting = settings.Query().FirstOrDefault();
                if (setting == null)
                {
                    var newSetting = new Settings {IsDarkMode = false};
                    settings.Insert(newSetting);
                    return newSetting;
                }
                return setting;
            }
        }

        public static void UpdateSettings(bool isDarkMode)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var settings = db.GetCollection<Settings>("settings");
                var set = settings.Query().FirstOrDefault();
                set.IsDarkMode = isDarkMode;
                settings.Update(set);
            }
        }
    }
}

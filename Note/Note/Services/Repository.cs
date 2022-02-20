using LiteDB;
using System.IO;

namespace Note.Services
{
    public class Repository : IRepository
    {
        private readonly LiteDatabase _database;
        private readonly string path = Path.Combine(System.Environment
            .GetFolderPath(System.Environment.SpecialFolder.Personal), "noteDB.db");

        public Repository()
        {
            _database = new LiteDatabase(path);
        }

        public void Save<T>(T item) =>
            GetCollection<T>().Upsert(item);

        public ILiteCollection<T> GetCollection<T>() => 
            _database.GetCollection<T>();
    }
}

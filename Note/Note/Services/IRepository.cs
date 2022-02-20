using LiteDB;

namespace Note.Services
{
    public interface IRepository
    {
        void Save<T>(T item);
        ILiteCollection<T> GetCollection<T>();
    }
}

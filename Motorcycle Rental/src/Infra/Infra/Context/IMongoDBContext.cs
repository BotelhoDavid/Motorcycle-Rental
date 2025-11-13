using MongoDB.Driver;

namespace Infra.Context
{
    public interface IMongoDBContext
    {
        public IMongoDatabase DataBase { get; }

        string GetConnectionStringFromEnvironment();
    }
}
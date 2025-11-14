using MongoDB.Driver;

namespace Infra.Data.Mongo.Context
{
    public interface IMongoContext
    {
        public IMongoDatabase DataBase { get; }

        string GetConnectionStringFromEnvironment();
    }
}
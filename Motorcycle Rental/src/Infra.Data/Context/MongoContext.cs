using MongoDB.Driver;
using System;
using System.Collections;

namespace Infra.Data.Mongo.Context
{
    public class MongoContext : IMongoContext
    {
        private string _catalog = string.Empty;

        public IMongoDatabase DataBase { get; }

        public MongoContext()
        {
            MongoClient cliente = new MongoClient(GetConnectionStringFromEnvironment());
            DataBase = cliente.GetDatabase(_catalog);
        }

        public string GetConnectionStringFromEnvironment()
        {
            IDictionary _envVars = Environment.GetEnvironmentVariables();

            string _dataSource = _envVars["DB_MONGO_DATA_SOURCE"].ToString();
            _catalog = _envVars["DB_MONGO_CATALOG"].ToString();
            string _user = _envVars["DB_MONGO_DATABASE_USER"].ToString();
            string _password = _envVars["DB_MONGO_DATABASE_USER_PASSWORD"].ToString();

            string _connectionString;
            if (_envVars["ASPNETCORE_ENVIRONMENT"].ToString().ToLower() == "development.local")
                _connectionString = $"mongodb://{_user}:{_password}@{_dataSource}";
            else
                _connectionString = $"mongodb://{_user}:{_password}@{_dataSource}@{_user}@";

            return _connectionString;
        }
    }
}

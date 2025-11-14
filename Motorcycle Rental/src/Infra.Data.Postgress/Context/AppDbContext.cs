using Domain.Models;
using Infra.Data.Postgress.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Infra.Data.Postgress.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.SeedData();


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(wh => wh.ClrType.BaseType == typeof(Entity)))
            {
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType);
                MethodInfo propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                MethodCallExpression isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));


                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));


                LambdaExpression lambda = Expression.Lambda(compareExpression, parameter);


                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Agora SetBasePath está disponível
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();


            optionsBuilder
            .UseNpgsql(GetConnectionStringFromEnvironment())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


            optionsBuilder.EnableSensitiveDataLogging();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public static string GetConnectionStringFromEnvironment()
        {
            IDictionary vars = Environment.GetEnvironmentVariables();


            string host = vars["DB_DATA_SOURCE"].ToString();
            string db = vars["DB_CATALOG"].ToString();
            string user = vars["DB_DATABASE_USER"].ToString();
            string password = vars["DB_DATABASE_USER_PASSWORD"].ToString();
            string timeout = vars["DB_DATABASE_TIMEOUT_SEGUNDOS"]?.ToString();


            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Database = db,
                Username = user,
                Password = password,
                Pooling = true,
                MaxPoolSize = 5000,
                Timeout = int.TryParse(timeout, out int t) ? t : (int)TimeSpan.FromMinutes(3).TotalSeconds,
                SslMode = SslMode.Disable,
                TrustServerCertificate = true
            };


            return builder.ConnectionString;
        }


        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (entry.Entity is Entity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = DateTime.UtcNow;
                        entity.IsDeleted = false;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.ModifiedDate = DateTime.UtcNow;
                    }
                }
            });
        }
    }
}
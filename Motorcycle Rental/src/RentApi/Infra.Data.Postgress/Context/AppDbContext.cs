using Rent.Infra.Data.Postgress.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Rent.Domain.Entities;
using Rent.Domain.Models;
using System.Collections;
using RentEntity = Rent.Domain.Entities.Rent;

namespace Rent.Infra.Data.Postgress.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Moto> Motos => Set<Moto>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<RentEntity> Rents => Set<RentEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoMap());
            modelBuilder.ApplyConfiguration(new DriverMap());
            modelBuilder.ApplyConfiguration(new RentMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                                                  .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: false)
                                                                  .Build();

            optionsBuilder.UseNpgsql(GetConnectionStringFromEnvironment(), opcao => opcao.EnableRetryOnFailure())
                          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            optionsBuilder.EnableSensitiveDataLogging();

            if (_environment.Equals("Development.Local"))
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole()
                           .AddFilter(level => level >= LogLevel.Information);
                });

                optionsBuilder.UseLoggerFactory(loggerFactory);
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public static string GetConnectionStringFromEnvironment()
        {
            IDictionary vars = Environment.GetEnvironmentVariables();

            string host = vars["DB_DATA_SOURCE"]?.ToString() ?? string.Empty;
            string db = vars["DB_CATALOG"]?.ToString() ?? string.Empty;
            string user = vars["DB_DATABASE_USER"]?.ToString() ?? string.Empty;
            string password = vars["DB_DATABASE_USER_PASSWORD"]?.ToString() ?? string.Empty;
            string timeout = vars["DB_DATABASE_TIMEOUT_SEGUNDOS"]?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(db))
                return string.Empty;

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Database = db,
                Username = user,
                Password = password,
                Pooling = true,
                MaxPoolSize = 5000,
                Timeout = int.TryParse(timeout, out int t) ? t : (int)TimeSpan.FromMinutes(3).TotalSeconds,
                SslMode = SslMode.Disable
            };

            return builder.ConnectionString;
        }
        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (entry.Entity is Entity trackableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        trackableEntity.CreatedDate = DateTime.Now;
                        trackableEntity.IsDeleted = false;
                    }

                    else if (entry.State == EntityState.Modified)
                        trackableEntity.ModifiedDate = DateTime.Now;
                }
            });
        }
    }
}
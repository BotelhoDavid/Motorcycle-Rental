using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Rent.Application.Interfaces;
using Rent.Application.Mapping;
using Rent.Application.Services;
using Rent.Domain.Interfaces.MessageBus;
using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Infra.CrossCutting.RabbitMQ.Producers;
using Rent.Infra.Data.Postgress.Context;
using Rent.Infra.Data.Postgress.Repositories;

namespace Rent.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region PostgreSQL
            services.AddDbContext<AppDbContext>();
            #endregion PostgreSQL

            #region RabbitMQ
            services.AddSingleton<IRabbitMqContext, RabbitMqContext>();
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

            #endregion RabbitMQ

            #region AppServices
            services.AddScoped<IDriverAppService, DriverAppService>();
            services.AddScoped<IMotoAppService, MotoAppService>();
            services.AddScoped<IRentAppService, RentAppService>();
            #endregion AppServices

            #region Repositories
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IRentRepository, RentRepository>();
            #endregion Repositories);

        }
    }
}
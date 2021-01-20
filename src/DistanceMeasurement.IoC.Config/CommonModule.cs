using Autofac;
using DistanceMeasurement.BusinessLogic;
using DistanceMeasurement.BusinessLogic.DistanceCalculator;
using DistanceMeasurement.BusinessLogic.PlacesCTeleport;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace DistanceMeasurement.IoC.Config
{
    public class CommonModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MemoryCache(new MemoryCacheOptions()))
                .As<IMemoryCache>()
                .SingleInstance();

            builder.Register(c => {
                
                var services = new ServiceCollection();
                
                services.AddHttpClient();
                
                var serviceProvider = services.BuildServiceProvider();
                
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                
                return httpClientFactory;
            }).SingleInstance();

            builder.RegisterType<DistanceCalculator>()
                .As<IDistanceCalculator>();

            builder.RegisterType<PlacesCTeleportClient>()
                .WithParameter("apiUrl", "https://places-dev.cteleport.com/airports")
                .As<IPlacesCTeleportClient>();

            builder.RegisterType<PlacesCTeleportRequestor>()
                .As<IPlacesCTeleportRequestor>();

            builder.RegisterType<MeasureDistanceRequestHandler>()
                .AsSelf();
        }
    }
}

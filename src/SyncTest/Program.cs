using Autofac;
using DistanceMeasurement.BusinessLogic;
using DistanceMeasurement.IoC.Config;
using Microsoft.Extensions.Logging;
using System;

namespace SyncTest
{
    class Program
    {
        private static ILifetimeScope _ioc;
        
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            _ioc = ConfigureContainer();

            var measureDistanceRequestHandler = _ioc.Resolve<MeasureDistanceRequestHandler>();
            
            double result = await measureDistanceRequestHandler.GetDistanceAsync("SVO", "DME");
        }

        private static ILifetimeScope ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());

            builder.Register(c => {

                var loggerFactory = new LoggerFactory();
                
                loggerFactory.AddConsole(LogLevel.Information);
                
                loggerFactory.AddDebug();

                return loggerFactory;
            }).As<ILoggerFactory>().SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>))
                            .As(typeof(ILogger<>))
                            .SingleInstance();
            
            return builder.Build();
        }
    }
}

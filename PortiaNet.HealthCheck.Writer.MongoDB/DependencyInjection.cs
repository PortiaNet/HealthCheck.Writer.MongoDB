using Microsoft.Extensions.DependencyInjection;
using PortiaNet.HealthCheck.Reporter;
using PortiaNet.HealthCheck.Writer.MongoDB;

namespace PortiaNet.HealthCheck.Writer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDBWriter(this IServiceCollection services, Action<MongoDBWriterConfiguration> configuration)
        {
            var config = new MongoDBWriterConfiguration();
            configuration(config);
            var reportServiceImplementation = new HealthCheckReportService(config);
            services.AddSingleton<IHealthCheckReportService>(reportServiceImplementation);
            return services;
        }
    }
}

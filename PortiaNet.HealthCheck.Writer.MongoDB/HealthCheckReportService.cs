using MongoDB.Driver;
using PortiaNet.HealthCheck.Reporter;
using System.Diagnostics;

namespace PortiaNet.HealthCheck.Writer.MongoDB
{
    internal class HealthCheckReportService : IHealthCheckReportService
    {
        private readonly MongoDBWriterConfiguration _config;
        private readonly IMongoCollection<RequestDetail>? _requestsCollection;

        public HealthCheckReportService(MongoDBWriterConfiguration config)
        {
            _config = config;
            try
            {
                var mongoClient = new MongoClient(config.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(config.DatabaseName);
                _requestsCollection = mongoDatabase.GetCollection<RequestDetail>(config.CollectionName);
            }
            catch(Exception ex)
            {
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);
                Debugger.Log(0, "HTTP Writer", $"Configuration Error :: {ex.Message}");
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);
                Debugger.Log(0, "HTTP Writer", ex.StackTrace);
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);

                if (!config.MuteOnError)
                    throw;
            }
        }

        public Task SaveAPICallInformationAsync(RequestDetail requestDetail)
        {
            if (_requestsCollection == null)
                return Task.CompletedTask;

            try
            {
                requestDetail.NodeName = _config.NodeName;
                return _requestsCollection.InsertOneAsync(requestDetail);
            }
            catch(Exception ex)
            {
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);
                Debugger.Log(0, "HTTP Writer", $"Error :: {ex.Message}");
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);
                Debugger.Log(0, "HTTP Writer", ex.StackTrace);
                Debugger.Log(0, "HTTP Writer", Environment.NewLine);

                if (!_config.MuteOnError)
                    throw;
                else
                    return Task.CompletedTask;
            }
        }
    }
}

using MongoDB.Driver;
using PortiaNet.HealthCheck.Reporter;

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
            catch
            {
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
            catch
            {
                if(!_config.MuteOnError)
                    throw;
                else
                    return Task.CompletedTask;
            }
        }
    }
}

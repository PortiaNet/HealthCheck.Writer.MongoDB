namespace PortiaNet.HealthCheck.Writer
{
    public class MongoDBWriterConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Default value is <b>HealthCheck</b>
        /// </summary>
        public string DatabaseName { get; set; } = "HealthCheck";

        /// <summary>
        /// Default value is <b>RequestTracks</b>
        /// </summary>
        public string CollectionName { get; set; } = "RequestTracks";

        public string NodeName { get; set; } = string.Empty;

        public bool MuteOnError { get; set; } = false;
    }
}

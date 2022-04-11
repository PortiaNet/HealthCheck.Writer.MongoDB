<img src="https://user-images.githubusercontent.com/11803924/159634426-45568864-0568-441c-9d0b-a28756f42f9f.png" width="400">


[![Nuget](https://img.shields.io/nuget/v/PortiaNet.HealthCheck.Writer.MongoDB?label=NuGet)](https://www.nuget.org/packages/PortiaNet.HealthCheck.Reporter/)

This library is the writer for the [PortiaNet.HealthCheck.Reporter](https://github.com/PortiaNet/HealthCheck.Reporter) and saves all reported logs to the configured MongoDB Database.

### Installation

You can install this tool from [Nuget](https://www.nuget.org/packages/PortiaNet.HealthCheck.Writer.MongoDB) using Package Manager Console:

```
PM> Install-Package PortiaNet.HealthCheck.Writer.MongoDB
```

### How do I get started?

After adding the [HealthCheck](https://github.com/PortiaNet/HealthCheck.Reporter/blob/master/PortiaNet.HealthCheck.Reporter/HealthCheckMiddleware.cs) middleware to the application pipeline, you need to add this library to DI and configure it as you prefer.

``` C#
using PortiaNet.HealthCheck.Writer;

var builder = WebApplication.CreateBuilder(args);
...
builder.Services.AddMongoDBWriter(options =>
{
    options.DatabaseName = "HealthCheck"; // Default Database Name
    options.ConnectionString = "mongodb://localhost:27017"; // Connection String to the MongoDB instance
    options.MuteOnError = false; // To suppress/not suppress the internal errors like DB Connection or privilege exceptions
    options.CollectionName = "RequestTracks"; // Default Collection Name
    options.NodeName = "Main Node"; // When having different instances of one service, this parameter will help you to distinguish between their logs in the database 
});
...
```
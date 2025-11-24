using Aspire.Hosting.Azure;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<MongoDBDatabaseResource> mongodb = builder
    .AddMongoDB("mongo")
    .WithDataVolume("kathanika-ils-mongo-data")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("mongodb", "kathanika_ils");

IResourceBuilder<RedisResource> redis = builder.AddRedis("redis");
IResourceBuilder<AzureBlobStorageResource> azureBlobStorage = builder.AddAzureStorage("azureBlobStorage")
    .RunAsEmulator()
    .AddBlobs("blobs");

IResourceBuilder<ProjectResource> webService = builder
    .AddProject<Projects.Kathanika_Web>("kathanika-web-service")
    .WithReference(mongodb)
    .WithReference(redis)
    .WithReference(azureBlobStorage)
    .WithOtlpExporter();

builder
    .AddNpmApp("kathanika-web-app", ".")
    .WithUrl("http://localhost:4200")
    .WithReference(webService)
    .WaitFor(webService);

await builder.Build().RunAsync();

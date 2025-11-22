using Aspire.Hosting.Azure;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<MongoDBServerResource> mongodb = builder.AddMongoDB("mongodb");
IResourceBuilder<RedisResource> redis = builder.AddRedis("redis");
IResourceBuilder<AzureBlobStorageResource> azureBlobStorage = builder.AddAzureStorage("azureBlobStorage")
    .RunAsEmulator()
    .AddBlobs("blobs");

IResourceBuilder<ProjectResource> webService = builder
    .AddProject<Projects.Kathanika_Web>("kathanika-web-service")
    .WithReference(mongodb)
    .WaitFor(mongodb)
    .WithReference(redis)
    .WaitFor(azureBlobStorage)
    .WithReference(azureBlobStorage)
    .WithOtlpExporter();

builder
    .AddNpmApp("kathanika-web-app", ".")
    .WithUrl("http://localhost:4200")
    .WithReference(webService)
    .WaitFor(webService);

await builder.Build().RunAsync();

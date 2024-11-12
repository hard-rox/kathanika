using Kathanika.Application.Features.BibRecords.Commands;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Graphql;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Infrastructure.Workers.Jobs;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Kathanika.Web.Tests;

public sealed class DependencyDirectionTests
{
    private readonly Assembly _domainAssembly = typeof(AggregateRoot).Assembly;

    [Fact]
    public void Domain_ShouldNot_HaveDependencyOnApplication()
    {
        Assembly applicationAssembly = typeof(CreateBibRecordCommand).Assembly;

        TestResult result = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(applicationAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Domain_ShouldNot_HaveDependencyOnInfrastructure()
    {
        Assembly graphqlInfraAssembly = typeof(SchemaConfigurations).Assembly;
        Assembly persistenceInfraAssembly = typeof(MongoDbConfigurations).Assembly;
        Assembly workersInfraAssembly = typeof(ProcessOutboxMessagesJob).Assembly;
        Assembly webAssembly = typeof(Program).Assembly;

        TestResult graphqlInfraDepsResult = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(graphqlInfraAssembly.GetName().Name)
            .GetResult();
        TestResult persistenceInfraDepsResult = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(persistenceInfraAssembly.GetName().Name)
            .GetResult();
        TestResult workerInfraDepsResult = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(workersInfraAssembly.GetName().Name)
            .GetResult();
        TestResult webResult = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(webAssembly.GetName().Name)
            .GetResult();

        Assert.True(graphqlInfraDepsResult.IsSuccessful);
        Assert.True(persistenceInfraDepsResult.IsSuccessful);
        Assert.True(workerInfraDepsResult.IsSuccessful);
        Assert.True(webResult.IsSuccessful);
    }

    [Fact]
    public void Application_ShouldNot_HaveDependencyOnInfrastructure()
    {
        Assembly applicationAssembly = typeof(CreateBibRecordCommand).Assembly;
        Assembly graphqlInfraAssembly = typeof(SchemaConfigurations).Assembly;
        Assembly persistenceInfraAssembly = typeof(MongoDbConfigurations).Assembly;
        Assembly workersInfraAssembly = typeof(ProcessOutboxMessagesJob).Assembly;

        TestResult graphqlInfraDepsResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(graphqlInfraAssembly.GetName().Name)
            .GetResult();
        TestResult persistenceInfraDepsResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(persistenceInfraAssembly.GetName().Name)
            .GetResult();
        TestResult workerInfraDepsResult = Types.InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(workersInfraAssembly.GetName().Name)
            .GetResult();

        Assert.True(graphqlInfraDepsResult.IsSuccessful);
        Assert.True(persistenceInfraDepsResult.IsSuccessful);
        Assert.True(workerInfraDepsResult.IsSuccessful);
    }
}
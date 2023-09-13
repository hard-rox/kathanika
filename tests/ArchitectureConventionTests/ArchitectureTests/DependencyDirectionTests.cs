using Kathanika.Application.Authors.Commands;
using Kathanika.Domain.Primitives;

namespace Kathanika.ArchitectureConventionTests.ArchitectureTests;

public sealed class DependencyDirectionTests
{
    private readonly Assembly _domainAssembly = typeof(AggregateRoot).Assembly;
    
    [Fact]
    public void Domain_ShouldNot_HaveDependencyOnApplication()
    {
        Assembly applicationAssembly = typeof(AddAuthorCommand).Assembly;

        TestResult result = Types.InAssembly(_domainAssembly)
        .ShouldNot()
        .HaveDependencyOn(applicationAssembly.GetName().Name)
        .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
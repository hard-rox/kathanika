using System.Reflection;
using MediatR;
using NetArchTest.Rules;

namespace Kathanika.Core.Application.Test.ArchTests;

public class ArchTests
{
    [Fact]
    public void CommandAndQueries_ShouldBeSealed()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        TestResult result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .Or()
            .ImplementInterface(typeof(INotification))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful, $"Non-sealed command/queries are: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Handlers_ShouldBeSealed()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        TestResult result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .ImplementInterface(typeof(INotificationHandler<>))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful, $"Non-sealed handlers are: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Handlers_ShouldEndsWithHandler()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        TestResult result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .ImplementInterface(typeof(INotificationHandler<>))
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.True(result.IsSuccessful, $"Non-sealed handlers are: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }
}

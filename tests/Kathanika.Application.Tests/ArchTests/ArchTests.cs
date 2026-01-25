using System.Reflection;
using Kathanika.Application.Abstractions.Messaging;
using MediatR;
using NetArchTest.Rules;

namespace Kathanika.Application.Tests.ArchTests;

public class ArchTests
{
    [Fact]
    public void CommandAndQueries_ShouldBeSealed()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        var types = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .Or()
            .ImplementInterface(typeof(INotification))
            .GetTypes()
            .Where(t => t.Namespace != "Kathanika.Application.Abstractions.Messaging")
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => !t.IsSealed);

        Assert.Empty(types.Select(t => t.FullName));
    }

    [Fact]
    public void Handlers_ShouldBeSealed()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        var types = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .ImplementInterface(typeof(INotificationHandler<>))
            .GetTypes()
            .Where(t => t.Namespace != "Kathanika.Application.Abstractions.Messaging")
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => !t.IsSealed);

        Assert.Empty(types.Select(t => t.FullName));
    }

    [Fact]
    public void Handlers_ShouldEndsWithHandler()
    {
        Assembly assembly = typeof(DependencyInjector).Assembly;

        var types = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .ImplementInterface(typeof(INotificationHandler<>))
            .GetTypes()
            .Where(t => t.Namespace != "Kathanika.Application.Abstractions.Messaging")
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => !t.Name.EndsWith("Handler"));

        Assert.Empty(types.Select(t => t.FullName));
    }
}
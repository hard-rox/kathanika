using Kathanika.Application.Abstractions;

namespace Kathanika.Infrastructure.Persistence.Implementations;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly IClientSessionHandle _session;
    private readonly List<Action> _operations;

    public UnitOfWork(IMongoClient mongoClient)
    {
        _session = mongoClient.StartSession();
        _operations = new List<Action>();
    }

    public void AddOperation(Action operation)
    {
        this._operations.Add(operation);
    }

    public void CleanOperations()
    {
        this._operations.Clear();
    }

    public async Task CommitChangesAsync()
    {
        _session.StartTransaction();
        _operations.ForEach(o =>
        {
            o.Invoke();
        });
        await _session.CommitTransactionAsync();

        this.CleanOperations();
    }
}
namespace Kathanika.Application.Abstractions;

public interface IUnitOfWork
{
    void AddOperation(Action operation);
    Task CommitChangesAsync();
}
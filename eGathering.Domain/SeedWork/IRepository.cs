namespace eGathering.Domain.SeedWork;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}
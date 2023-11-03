using eGathering.Domain.SeedWork;
using eGathering.Domain.ValueObjects;

namespace eGathering.Domain.Members;

public interface IMemberCommandRepository : IRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Member?> GetByFullNameForValidateAsync(FullName fullName, CancellationToken cancellationToken);

    Task<Guid> CreateAsync(Member member, CancellationToken cancellationToken);

    bool Update(Member member);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken);
}
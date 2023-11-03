using eGathering.Domain.Attendees;
using eGathering.Domain.Gatherings;
using eGathering.Domain.Invitations;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using eGathering.Persistence.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eGathering.Persistence;

public class GatheringContext : DbContext, IUnitOfWork
{
    private const string Collation = "Chinese_Taiwan_Stroke_CS_AS";
    private readonly IMediator _mediator;
    private IDbContextTransaction? _currentTransaction;

    public GatheringContext(DbContextOptions<GatheringContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Member> Members { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<Invitation> Invitations { get; set; }

    public DbSet<Gathering> Gatherings { get; set; }

    public IDbContextTransaction? GetCurrentTransaction
        => _currentTransaction;

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
        CorrectTransaction(transaction);
        await ExecuteCommitAsync(transaction).ConfigureAwait(false);
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return null!;
        }

        _currentTransaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted)
                                            .ConfigureAwait(false);
        return _currentTransaction;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this, cancellationToken)
                        .ConfigureAwait(false);
        var result = await this.SaveChangesAsync(cancellationToken)
                                .ConfigureAwait(false);
        return result > 0;
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation(Collation);
        modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GatheringEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationEntityTypeConfiguration());
    }

    private void CorrectTransaction(IDbContextTransaction transaction)
    {
        if (transaction is null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        if (transaction != _currentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        }
    }

    private async Task ExecuteCommitAsync(IDbContextTransaction transaction)
    {
        try
        {
            await SaveChangesAsync().ConfigureAwait(false);
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
using eGathering.Domain.SeedWork;
using MediatR;
using Polly;

namespace eGathering.Persistence;

internal static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, GatheringContext context, CancellationToken cancellationToken)
    {
        var events = context.ChangeTracker
                                    .Entries<AggregateRoot>()
                                    .Select(x => x.Entity)
                                    .SelectMany(aggregateRoot =>
                                    {
                                        var domainEvents = aggregateRoot.GetDomainEvents();
                                        aggregateRoot.ClearDomainEvents();
                                        return domainEvents;
                                    })
                                    .ToList();

        foreach (var domainEvent in events)
        {
            var policy = Policy
                   .Handle<Exception>()
                   .WaitAndRetryAsync(
                        3,
                        attempt => TimeSpan.FromMilliseconds(50 * attempt));

            var result = await policy.ExecuteAndCaptureAsync(() =>
                            mediator.Publish(
                                domainEvent,
                                cancellationToken)).ConfigureAwait(false);

            if (result.FinalException is not null)
            {
                throw result.FinalException;
            }
        }
    }
}
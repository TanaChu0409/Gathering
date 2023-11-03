using eGathering.Domain.Gatherings;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;

namespace eGathering.Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
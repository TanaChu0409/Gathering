using eGathering.Domain.Gatherings;
using eGathering.Domain.Members;

namespace eGathering.Domain.SeedWork;

public interface IEmailService
{
    Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken);
    Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken);
}
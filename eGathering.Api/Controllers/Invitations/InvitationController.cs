using eGathering.Application.Invitations.Commands.AcceptInvitation;
using eGathering.Application.Invitations.Commands.SendInvitation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eGathering.Api.Controllers.Invitations;

[Route("api/invitation")]
[ApiController]
public sealed class InvitationController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvitationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("accept/{gatheringId}/{invitationId}")]
    [HttpPatch]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AcceptInvitationAsync([FromRoute] Guid gatheringId, [FromRoute] Guid invitationId, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new AcceptInvitationCommand(
                gatheringId,
                invitationId),
            cancellationToken)
            .ConfigureAwait(false);
        return Ok();
    }

    [Route("send")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SendInvitationAsync([FromBody] SendInvitationRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new SendInvitationCommand(
                request.MemberId,
                request.GatheringId),
            cancellationToken)
            .ConfigureAwait(false);
        return Ok();
    }
}
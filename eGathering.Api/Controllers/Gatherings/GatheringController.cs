using eGathering.Application.Gatherings.Commands.CreateGathering;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eGathering.Api.Controllers.Gatherings;

[Route("api/gathering")]
[ApiController]
public sealed class GatheringController : ControllerBase
{
    private readonly IMediator _mediator;

    public GatheringController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("{memberId}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateGatheringAsync([FromRoute] Guid memberId, [FromBody] GatheringRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateGatheringCommand(
                memberId,
                request.Type,
                request.ScheduledAtUtc,
                request.Name,
                request.Location,
                request.MaximumNumberOfAttendees,
                request.InvitationsValidBeforeInHours),
            cancellationToken)
            .ConfigureAwait(false);
        return Ok();
    }
}
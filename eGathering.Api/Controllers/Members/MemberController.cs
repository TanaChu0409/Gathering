using eGathering.Api.Abstractions;
using eGathering.Application.Members.Commands.CreateMember;
using eGathering.Application.Members.Commands.UpdateMember;
using eGathering.Application.Members.Queries.Dto;
using eGathering.Application.Members.Queries.GetMemberDetail;
using eGathering.Application.Members.Queries.GetMembers;
using eGathering.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eGathering.Api.Controllers.Members;

[Route("api/member")]
public sealed class MemberController : ApiController
{
    public MemberController(ISender sender)
        : base(sender)
    {
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IResult> CreateMemberAsync([FromBody] MemberCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(
            new CreateMemberCommand(
                request.FirstName,
                request.LastName,
                request.Email),
            cancellationToken)
            .ConfigureAwait(false);
        return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: (error) => Results.BadRequest(error));
    }

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetMembersAsync([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? email, CancellationToken cancellationToken)
    {
        var membersDto = await Sender.Send(
            new GetMembersWithConditionQuery(
                firstName,
                lastName,
                email),
            cancellationToken)
            .ConfigureAwait(false);

        return membersDto is null ? NoContent() : Ok(membersDto);
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(MemberDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetMember([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var member = await Sender.Send(new GetMemberDetailQuery(id), cancellationToken)
                                 .ConfigureAwait(false);
        return member is null ? NotFound() : Ok(member);
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateMember([FromRoute] Guid id, [FromBody] MemberUpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        if (id != updateRequest.Id)
        {
            return BadRequest();
        }

        await Sender.Send(
            new UpdateMemberCommand(
                updateRequest.Id,
                updateRequest.FirstName,
                updateRequest.LastName,
                updateRequest.Email),
            cancellationToken)
            .ConfigureAwait(false);
        return Ok();
    }
}
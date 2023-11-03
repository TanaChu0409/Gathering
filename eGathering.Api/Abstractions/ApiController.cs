using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eGathering.Api.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    internal readonly ISender Sender;

    protected ApiController(ISender sender)
        => Sender = sender;
}
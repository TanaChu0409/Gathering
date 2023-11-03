using eGathering.Domain.Shared;
using MediatR;

namespace eGathering.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
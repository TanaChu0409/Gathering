using eGathering.Domain.Shared;
using MediatR;

namespace eGathering.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
using FluentResults;
using MediatR;

namespace SmartSalon.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{ }

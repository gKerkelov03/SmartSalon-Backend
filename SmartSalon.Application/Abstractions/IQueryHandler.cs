using MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{ }

using MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : IRequest<Result>
{ }

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{ }

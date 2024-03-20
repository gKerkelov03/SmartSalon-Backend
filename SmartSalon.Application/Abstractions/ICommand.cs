using FluentResults;
using MediatR;

namespace SmartSalon.Application.Abstractions;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResult> : IRequest<Result<TResult>> { }

using FluentResults;
using MediatR;

namespace SmartSalon.Application.Abstractions;

public interface IQuery<TResult> : IRequest<Result<TResult>> { }

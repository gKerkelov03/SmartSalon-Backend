using MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Abstractions;

public interface IQuery<TResult> : IRequest<Result<TResult>> { }

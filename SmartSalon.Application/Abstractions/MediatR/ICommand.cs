using MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Abstractions.MediatR;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TValue> : IRequest<Result<TValue>> { }

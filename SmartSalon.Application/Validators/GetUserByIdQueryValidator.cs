using FluentValidation;
using SmartSalon.Application.Queries.Handlers;

namespace SmartSalon.Application.Validators;

internal class GetByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetByIdQueryValidator()
    {
    }
}
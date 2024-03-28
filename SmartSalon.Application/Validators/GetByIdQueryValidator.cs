using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

internal class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
    }
}
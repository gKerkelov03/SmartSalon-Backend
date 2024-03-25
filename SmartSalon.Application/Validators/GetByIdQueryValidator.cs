using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
    }
}
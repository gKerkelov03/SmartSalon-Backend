using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

class GetAllQueryValidator : AbstractValidator<GetAllQuery>
{
    public GetAllQueryValidator()
    {
    }
}
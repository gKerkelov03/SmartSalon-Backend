using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

internal class GetAllQueryValidator : AbstractValidator<GetAllQuery>
{
    public GetAllQueryValidator()
    {
    }
}
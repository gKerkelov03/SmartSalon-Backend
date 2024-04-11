using FluentValidation;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Validators;

internal class SearchForUnemployedWorkerQueryValidator : AbstractValidator<SearchForUnemployedWorkerQuery>
{
    public SearchForUnemployedWorkerQueryValidator()
    {
        RuleFor(query => query.SearchTerm).NotEmpty();
    }
}
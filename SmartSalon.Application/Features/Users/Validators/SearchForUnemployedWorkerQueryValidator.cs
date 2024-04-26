using FluentValidation;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Features.Users.Validators;

internal class SearchForUnemployedWorkerQueryValidator : AbstractValidator<SearchForUnemployedWorkerQuery>
{
    public SearchForUnemployedWorkerQueryValidator()
    {
        RuleFor(query => query.SearchTerm).NotEmpty();
    }
}
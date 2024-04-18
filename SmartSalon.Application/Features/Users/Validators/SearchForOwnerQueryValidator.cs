using FluentValidation;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Application.Validators;

internal class SearchForOwnerQueryValidator : AbstractValidator<SearchForOwnerQuery>
{
    public SearchForOwnerQueryValidator()
    {
        RuleFor(query => query.SearchTerm).NotEmpty();
    }
}
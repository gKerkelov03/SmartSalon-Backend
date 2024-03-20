using FluentValidation;
using SmartSalon.Application.Queries;

namespace SmartSalon.Application.Validators;

class ExampleQueryValidator : AbstractValidator<ExampleQuery>
{
    public ExampleQueryValidator()
    {
        RuleFor(x => x.ExampleProperty1).NotNull().NotEmpty();
        RuleFor(x => x.ExampleProperty2).GreaterThan(5);
    }
}
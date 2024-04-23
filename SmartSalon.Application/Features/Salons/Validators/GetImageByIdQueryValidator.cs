
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Application.Validators;

internal class GetImageByIdQueryValidator : AbstractValidator<GetImageByIdQuery>
{
    public GetImageByIdQueryValidator()
    {
        RuleFor(query => query.ImageId).MustBeValidGuid();
    }
}

using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Application.Features.Services.Validators;

internal class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
{
    public GetServiceByIdQueryValidator()
    {
        RuleFor(query => query.ServiceId).MustBeValidGuid();
    }
}
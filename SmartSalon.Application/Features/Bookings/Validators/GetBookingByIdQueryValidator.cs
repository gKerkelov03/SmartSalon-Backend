
using FluentValidation;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
{
    public GetBookingByIdQueryValidator()
    {
    }
}
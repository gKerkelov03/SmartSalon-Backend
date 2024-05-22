
using FluentValidation;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
{
    public GetBookingByIdQueryValidator() => RuleFor(query => query.BookingId).MustBeValidGuid();
}
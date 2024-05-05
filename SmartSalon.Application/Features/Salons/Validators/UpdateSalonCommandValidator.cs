
using FluentValidation;
using SmartSalon.Application.Features.Salons.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Application.Features.Salons.Validators;

internal class UpdateSalonCommandValidator : AbstractValidator<UpdateSalonCommand>
{
    public UpdateSalonCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(MaxNameLength);
        RuleFor(command => command.Description).MaximumLength(MaxDescriptionLength);
        RuleFor(command => command.Location).MaximumLength(MaxLocationLength);
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
        RuleFor(command => command.TimePenalty).LessThan(MaxTimePenalty);
        RuleFor(command => command.BookingsInAdvance).LessThan(MaxBookingsInAdvance);
        RuleFor(command => command.SubscriptionsEnabled).NotNull();
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
        RuleFor(command => command.WorkersCanMoveBookings).NotNull();
        RuleFor(command => command.WorkersCanSetNonWorkingPeriods).NotNull();
    }
}
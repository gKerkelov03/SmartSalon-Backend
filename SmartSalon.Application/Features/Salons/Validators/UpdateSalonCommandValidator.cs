
using FluentValidation;
using SmartSalon.Application.Features.Salons.Commands;
using static SmartSalon.Application.ApplicationConstants.Validation.Salon;

namespace SmartSalon.Application.Validators;

internal class UpdateSalonCommandValidator : AbstractValidator<UpdateSalonCommand>
{
    public UpdateSalonCommandValidator()
    {
        RuleFor(command => command.Name).MinimumLength(MaxNameLength);
        RuleFor(command => command.Description).MaximumLength(MaxDescriptionLength);
        RuleFor(command => command.Location).MaximumLength(MaxLocationLength);
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
        RuleFor(command => command.DefaultTimePenalty).LessThan(MaxDefaultTimePenalty);
        RuleFor(command => command.DefaultBookingsInAdvance).LessThan(MaxDefaultBookingsInAdvance);
        RuleFor(command => command.SubscriptionsEnabled).NotNull();
        RuleFor(command => command.ProfilePictureUrl).NotEmpty();
        RuleFor(command => command.WorkersCanMoveBookings).NotNull();
        RuleFor(command => command.WorkersCanSetNonWorkingPeriods).NotNull();
    }
}
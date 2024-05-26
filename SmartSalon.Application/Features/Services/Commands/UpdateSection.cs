
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSection.Application.Features.Services.Commands;

public class UpdateSectionCommand : ICommand
{
    public required Id SectionId { get; set; }
    public required Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
    public int Order { get; set; }
}

internal class UpdateSectionCommandHandler(
    IEfRepository<Section> _sections,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateSectionCommand>
{
    public async Task<Result> Handle(UpdateSectionCommand command, CancellationToken cancellationToken)
    {
        var section = await _sections.GetByIdAsync(command.SectionId);

        if (section is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Include(salon => salon.Sections)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        if (section.Name != command.Name)
        {
            var salonAlreadyContainsSection = salon.Sections!
                .Any(existingSection => existingSection.Name == command.Name);

            if (salonAlreadyContainsSection)
            {
                return Error.Conflict;
            }

        }

        section.MapAgainst(command);
        _sections.Update(section);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

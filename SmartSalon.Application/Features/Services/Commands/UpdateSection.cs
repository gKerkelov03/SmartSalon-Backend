
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSection.Application.Features.Services.Commands;

public class UpdateSectionCommand : ICommand
{
    public required Id SectionId { get; set; }
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
}

internal class UpdateSectionCommandHandler(IEfRepository<Section> _sections, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateSectionCommand>
{
    public async Task<Result> Handle(UpdateSectionCommand command, CancellationToken cancellationToken)
    {
        var section = await _sections.GetByIdAsync(command.SectionId);

        if (section is null)
        {
            return Error.NotFound;
        }

        section.MapAgainst(command);
        _sections.Update(section);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

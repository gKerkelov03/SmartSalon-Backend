﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class CreateSectionCommand : ICommand<CreateSectionCommandResponse>, IMapTo<Section>
{
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
    public Id SalonId { get; set; }
}

public class CreateSectionCommandResponse(Id id)
{
    public Id CreatedSectionId => id;
}

internal class CreateSectionCommandHandler(
    IEfRepository<Section> _sections,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateSectionCommand, CreateSectionCommandResponse>
{
    public async Task<Result<CreateSectionCommandResponse>> Handle(CreateSectionCommand command, CancellationToken cancellationToken)
    {
        //TODO: set order last
        var newSection = _mapper.Map<Section>(command);

        var salon = await _salons.All
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsCategory = salon.Categories!.Any();

        if (salonAlreadyContainsCategory)
        {
            return Error.Conflict;
        }

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Sectionss!.Add(newService);

        await _sections.AddAsync(newSection);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateSectionCommandResponse(newSection.Id);
    }
}
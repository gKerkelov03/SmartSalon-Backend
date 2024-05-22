
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class CreateSalonResponse : IMapFrom<CreateSalonCommandResponse>
{
    public required Id CreatedSalonId { get; set; }
}


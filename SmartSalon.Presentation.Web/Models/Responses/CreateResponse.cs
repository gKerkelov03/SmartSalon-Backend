
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Presentation.Web.Models.Responses;

public class CreateResponse : IMapFrom<CreateCommandResponse>
{
    public required Id Id { get; set; }
}


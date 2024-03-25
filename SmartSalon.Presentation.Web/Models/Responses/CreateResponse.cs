
using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries;

namespace SmartSalon.Presentation.Web.Models.Responses;

public class CreateResponse : IMapFrom<CreateCommandResponse>
{
    public required Id Id { get; set; }
}


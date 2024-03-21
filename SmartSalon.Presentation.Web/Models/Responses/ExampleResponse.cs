using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries;

namespace SmartSalon.Presentation.Web.Models.Responses;

public class ExampleResponse : IMapFrom<ExampleQueryResponse>
{
    public required string ExampleProperty1 { get; set; }

    public required int ExampleProperty2 { get; set; }
}


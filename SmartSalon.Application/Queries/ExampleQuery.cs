using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Queries;

public class ExampleQuery : IQuery<ExampleQueryResponseModel>
{
    public required string ExampleProperty1 { get; set; }

    public required int ExampleProperty2 { get; set; }
}
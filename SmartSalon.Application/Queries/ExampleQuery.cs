using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;

namespace SmartSalon.Application.Queries;

public class ExampleQuery : IQuery<BookingTime>
{
    public required string ExampleProperty1 { get; set; }

    public required int ExampleProperty2 { get; set; }
}
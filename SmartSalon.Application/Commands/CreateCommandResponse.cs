
namespace SmartSalon.Application.Queries;

public class CreateCommandResponse
{
    public Id Id { get; set; }

    public CreateCommandResponse(Id id)
        => Id = id;
}
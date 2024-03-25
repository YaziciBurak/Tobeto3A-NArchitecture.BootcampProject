using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationEntities.Commands.Update;

public class UpdatedApplicationEntityResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public int ApplicationStateId { get; set; }
}

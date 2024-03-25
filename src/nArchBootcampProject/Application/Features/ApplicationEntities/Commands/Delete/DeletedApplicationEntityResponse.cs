using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationEntities.Commands.Delete;

public class DeletedApplicationEntityResponse : IResponse
{
    public int Id { get; set; }
}

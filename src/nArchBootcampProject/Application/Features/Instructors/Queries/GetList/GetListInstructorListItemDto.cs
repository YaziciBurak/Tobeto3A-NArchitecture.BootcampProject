using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Instructors.Queries.GetList;

public class GetListInstructorListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
}

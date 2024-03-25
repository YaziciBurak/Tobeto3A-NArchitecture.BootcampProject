using Application.Features.ApplicationEntities.Commands.Create;
using Application.Features.ApplicationEntities.Commands.Delete;
using Application.Features.ApplicationEntities.Commands.Update;
using Application.Features.ApplicationEntities.Queries.GetById;
using Application.Features.ApplicationEntities.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ApplicationEntities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationEntity, CreateApplicationEntityCommand>().ReverseMap();
        CreateMap<ApplicationEntity, CreatedApplicationEntityResponse>().ReverseMap();
        CreateMap<ApplicationEntity, UpdateApplicationEntityCommand>().ReverseMap();
        CreateMap<ApplicationEntity, UpdatedApplicationEntityResponse>().ReverseMap();
        CreateMap<ApplicationEntity, DeleteApplicationEntityCommand>().ReverseMap();
        CreateMap<ApplicationEntity, DeletedApplicationEntityResponse>().ReverseMap();
        CreateMap<ApplicationEntity, GetByIdApplicationEntityResponse>().ReverseMap();
        CreateMap<ApplicationEntity, GetListApplicationEntityListItemDto>().ReverseMap();
        CreateMap<IPaginate<ApplicationEntity>, GetListResponse<GetListApplicationEntityListItemDto>>().ReverseMap();
    }
}

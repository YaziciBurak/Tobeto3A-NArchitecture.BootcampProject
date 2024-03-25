using Application.Features.ApplicationEntities.Constants;
using Application.Features.ApplicationEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.ApplicationEntities.Constants.ApplicationEntitiesOperationClaims;

namespace Application.Features.ApplicationEntities.Queries.GetById;

public class GetByIdApplicationEntityQuery : IRequest<GetByIdApplicationEntityResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdApplicationEntityQueryHandler
        : IRequestHandler<GetByIdApplicationEntityQuery, GetByIdApplicationEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationEntityRepository _applicationEntityRepository;
        private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

        public GetByIdApplicationEntityQueryHandler(
            IMapper mapper,
            IApplicationEntityRepository applicationEntityRepository,
            ApplicationEntityBusinessRules applicationEntityBusinessRules
        )
        {
            _mapper = mapper;
            _applicationEntityRepository = applicationEntityRepository;
            _applicationEntityBusinessRules = applicationEntityBusinessRules;
        }

        public async Task<GetByIdApplicationEntityResponse> Handle(
            GetByIdApplicationEntityQuery request,
            CancellationToken cancellationToken
        )
        {
            ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
                predicate: ae => ae.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationEntityBusinessRules.ApplicationEntityShouldExistWhenSelected(applicationEntity);

            GetByIdApplicationEntityResponse response = _mapper.Map<GetByIdApplicationEntityResponse>(applicationEntity);
            return response;
        }
    }
}

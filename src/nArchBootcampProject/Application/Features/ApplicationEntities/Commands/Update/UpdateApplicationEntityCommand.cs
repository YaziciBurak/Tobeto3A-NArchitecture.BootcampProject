using Application.Features.ApplicationEntities.Constants;
using Application.Features.ApplicationEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.ApplicationEntities.Constants.ApplicationEntitiesOperationClaims;

namespace Application.Features.ApplicationEntities.Commands.Update;

public class UpdateApplicationEntityCommand
    : IRequest<UpdatedApplicationEntityResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public int ApplicationStateId { get; set; }

    public string[] Roles => [Admin, Write, ApplicationEntitiesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationEntities"];

    public class UpdateApplicationEntityCommandHandler
        : IRequestHandler<UpdateApplicationEntityCommand, UpdatedApplicationEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationEntityRepository _applicationEntityRepository;
        private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

        public UpdateApplicationEntityCommandHandler(
            IMapper mapper,
            IApplicationEntityRepository applicationEntityRepository,
            ApplicationEntityBusinessRules applicationEntityBusinessRules
        )
        {
            _mapper = mapper;
            _applicationEntityRepository = applicationEntityRepository;
            _applicationEntityBusinessRules = applicationEntityBusinessRules;
        }

        public async Task<UpdatedApplicationEntityResponse> Handle(
            UpdateApplicationEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
                predicate: ae => ae.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationEntityBusinessRules.ApplicationEntityShouldExistWhenSelected(applicationEntity);
            applicationEntity = _mapper.Map(request, applicationEntity);

            await _applicationEntityRepository.UpdateAsync(applicationEntity!);

            UpdatedApplicationEntityResponse response = _mapper.Map<UpdatedApplicationEntityResponse>(applicationEntity);
            return response;
        }
    }
}

using Application.Features.ApplicationEntities.Constants;
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

namespace Application.Features.ApplicationEntities.Commands.Delete;

public class DeleteApplicationEntityCommand
    : IRequest<DeletedApplicationEntityResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ApplicationEntitiesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationEntities"];

    public class DeleteApplicationEntityCommandHandler
        : IRequestHandler<DeleteApplicationEntityCommand, DeletedApplicationEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationEntityRepository _applicationEntityRepository;
        private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

        public DeleteApplicationEntityCommandHandler(
            IMapper mapper,
            IApplicationEntityRepository applicationEntityRepository,
            ApplicationEntityBusinessRules applicationEntityBusinessRules
        )
        {
            _mapper = mapper;
            _applicationEntityRepository = applicationEntityRepository;
            _applicationEntityBusinessRules = applicationEntityBusinessRules;
        }

        public async Task<DeletedApplicationEntityResponse> Handle(
            DeleteApplicationEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
                predicate: ae => ae.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationEntityBusinessRules.ApplicationEntityShouldExistWhenSelected(applicationEntity);

            await _applicationEntityRepository.DeleteAsync(applicationEntity!);

            DeletedApplicationEntityResponse response = _mapper.Map<DeletedApplicationEntityResponse>(applicationEntity);
            return response;
        }
    }
}

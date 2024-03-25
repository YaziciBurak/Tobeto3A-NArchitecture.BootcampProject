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

namespace Application.Features.ApplicationEntities.Commands.Create;

public class CreateApplicationEntityCommand
    : IRequest<CreatedApplicationEntityResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public int ApplicationStateId { get; set; }

    public string[] Roles => [Admin, Write, ApplicationEntitiesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationEntities"];

    public class CreateApplicationEntityCommandHandler
        : IRequestHandler<CreateApplicationEntityCommand, CreatedApplicationEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationEntityRepository _applicationEntityRepository;
        private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

        public CreateApplicationEntityCommandHandler(
            IMapper mapper,
            IApplicationEntityRepository applicationEntityRepository,
            ApplicationEntityBusinessRules applicationEntityBusinessRules
        )
        {
            _mapper = mapper;
            _applicationEntityRepository = applicationEntityRepository;
            _applicationEntityBusinessRules = applicationEntityBusinessRules;
        }

        public async Task<CreatedApplicationEntityResponse> Handle(
            CreateApplicationEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationEntity applicationEntity = _mapper.Map<ApplicationEntity>(request);

            await _applicationEntityRepository.AddAsync(applicationEntity);

            CreatedApplicationEntityResponse response = _mapper.Map<CreatedApplicationEntityResponse>(applicationEntity);
            return response;
        }
    }
}

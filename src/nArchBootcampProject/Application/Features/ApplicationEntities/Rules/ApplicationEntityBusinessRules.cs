using Application.Features.ApplicationEntities.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.ApplicationEntities.Rules;

public class ApplicationEntityBusinessRules : BaseBusinessRules
{
    private readonly IApplicationEntityRepository _applicationEntityRepository;
    private readonly ILocalizationService _localizationService;

    public ApplicationEntityBusinessRules(
        IApplicationEntityRepository applicationEntityRepository,
        ILocalizationService localizationService
    )
    {
        _applicationEntityRepository = applicationEntityRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(
            messageKey,
            ApplicationEntitiesBusinessMessages.SectionName
        );
        throw new BusinessException(message);
    }

    public async Task ApplicationEntityShouldExistWhenSelected(ApplicationEntity? applicationEntity)
    {
        if (applicationEntity == null)
            await throwBusinessException(ApplicationEntitiesBusinessMessages.ApplicationEntityNotExists);
    }

    public async Task ApplicationEntityIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
            predicate: ae => ae.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ApplicationEntityShouldExistWhenSelected(applicationEntity);
    }
}

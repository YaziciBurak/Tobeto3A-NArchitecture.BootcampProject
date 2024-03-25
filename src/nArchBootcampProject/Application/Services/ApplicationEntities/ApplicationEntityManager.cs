using System.Linq.Expressions;
using Application.Features.ApplicationEntities.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.ApplicationEntities;

public class ApplicationEntityManager : IApplicationEntityService
{
    private readonly IApplicationEntityRepository _applicationEntityRepository;
    private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

    public ApplicationEntityManager(
        IApplicationEntityRepository applicationEntityRepository,
        ApplicationEntityBusinessRules applicationEntityBusinessRules
    )
    {
        _applicationEntityRepository = applicationEntityRepository;
        _applicationEntityBusinessRules = applicationEntityBusinessRules;
    }

    public async Task<ApplicationEntity?> GetAsync(
        Expression<Func<ApplicationEntity, bool>> predicate,
        Func<IQueryable<ApplicationEntity>, IIncludableQueryable<ApplicationEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return applicationEntity;
    }

    public async Task<IPaginate<ApplicationEntity>?> GetListAsync(
        Expression<Func<ApplicationEntity, bool>>? predicate = null,
        Func<IQueryable<ApplicationEntity>, IOrderedQueryable<ApplicationEntity>>? orderBy = null,
        Func<IQueryable<ApplicationEntity>, IIncludableQueryable<ApplicationEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ApplicationEntity> applicationEntityList = await _applicationEntityRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return applicationEntityList;
    }

    public async Task<ApplicationEntity> AddAsync(ApplicationEntity applicationEntity)
    {
        ApplicationEntity addedApplicationEntity = await _applicationEntityRepository.AddAsync(applicationEntity);

        return addedApplicationEntity;
    }

    public async Task<ApplicationEntity> UpdateAsync(ApplicationEntity applicationEntity)
    {
        ApplicationEntity updatedApplicationEntity = await _applicationEntityRepository.UpdateAsync(applicationEntity);

        return updatedApplicationEntity;
    }

    public async Task<ApplicationEntity> DeleteAsync(ApplicationEntity applicationEntity, bool permanent = false)
    {
        ApplicationEntity deletedApplicationEntity = await _applicationEntityRepository.DeleteAsync(applicationEntity);

        return deletedApplicationEntity;
    }
}

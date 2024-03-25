using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.ApplicationEntities;

public interface IApplicationEntityService
{
    Task<ApplicationEntity?> GetAsync(
        Expression<Func<ApplicationEntity, bool>> predicate,
        Func<IQueryable<ApplicationEntity>, IIncludableQueryable<ApplicationEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ApplicationEntity>?> GetListAsync(
        Expression<Func<ApplicationEntity, bool>>? predicate = null,
        Func<IQueryable<ApplicationEntity>, IOrderedQueryable<ApplicationEntity>>? orderBy = null,
        Func<IQueryable<ApplicationEntity>, IIncludableQueryable<ApplicationEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ApplicationEntity> AddAsync(ApplicationEntity applicationEntity);
    Task<ApplicationEntity> UpdateAsync(ApplicationEntity applicationEntity);
    Task<ApplicationEntity> DeleteAsync(ApplicationEntity applicationEntity, bool permanent = false);
}

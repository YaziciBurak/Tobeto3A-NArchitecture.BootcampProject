using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ApplicationEntityRepository : EfRepositoryBase<ApplicationEntity, int, BaseDbContext>, IApplicationEntityRepository
{
    public ApplicationEntityRepository(BaseDbContext context)
        : base(context) { }
}

using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IApplicationEntityRepository : IAsyncRepository<ApplicationEntity, int>, IRepository<ApplicationEntity, int> { }

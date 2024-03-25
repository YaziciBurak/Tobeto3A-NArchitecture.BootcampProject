using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ApplicationState : Entity<int>
{
    public string Name { get; set; }
    public virtual ICollection<ApplicationEntity> Applications { get; set; }

    public ApplicationState()
    {
        Applications = new HashSet<ApplicationEntity>();
    }

    public ApplicationState(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

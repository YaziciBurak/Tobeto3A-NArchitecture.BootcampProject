using System.Reflection.Metadata;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Applicant : User
{
    public string About { get; set; }
    public virtual BlackList? BlackList { get; set; }
    public virtual ICollection<ApplicationEntity> Applications { get; set; }

    public Applicant()
    {
        Applications = new HashSet<ApplicationEntity>();
    }

    public Applicant(Guid id, string about)
    {
        Id = id;
        About = about;
    }
}

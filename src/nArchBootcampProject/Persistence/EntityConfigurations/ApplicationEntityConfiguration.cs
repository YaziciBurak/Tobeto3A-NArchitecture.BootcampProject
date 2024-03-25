using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.ToTable("ApplicationEntities").HasKey(ae => ae.Id);

        builder.Property(ae => ae.Id).HasColumnName("Id").IsRequired();
        builder.Property(ae => ae.ApplicantId).HasColumnName("ApplicantId");
        builder.Property(ae => ae.BootcampId).HasColumnName("BootcampId");
        builder.Property(ae => ae.ApplicationStateId).HasColumnName("ApplicationStateId");
        builder.Property(ae => ae.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ae => ae.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ae => ae.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ae => !ae.DeletedDate.HasValue);
        builder.HasOne(x => x.Bootcamp);
        builder.HasOne(x => x.Applicant);
        builder.HasOne(x => x.ApplicationState);
    }
}

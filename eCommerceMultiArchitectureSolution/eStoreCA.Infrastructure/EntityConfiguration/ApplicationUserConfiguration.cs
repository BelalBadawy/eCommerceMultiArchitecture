

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eStoreCA.Domain.Entities;

namespace eStoreCA.Infrastructure.EntityConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Configure the primary key
            builder.HasKey(u => u.Id);

            // Configure properties
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.CreatedDate)
                .IsRequired();

            // Table mapping (optional)
            builder.ToTable("AspNetUsers");
            #region Custom
            #endregion Custom

        }
    }
}

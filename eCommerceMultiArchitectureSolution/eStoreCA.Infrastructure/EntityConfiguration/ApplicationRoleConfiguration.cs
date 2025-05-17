

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eStoreCA.Domain.Entities;

namespace eStoreCA.Infrastructure.EntityConfiguration
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Configure the primary key
            builder.HasKey(u => u.Id);



            // Table mapping (optional)
            builder.ToTable("AspNetRoles");
            #region Custom
            #endregion Custom

        }
    }
}

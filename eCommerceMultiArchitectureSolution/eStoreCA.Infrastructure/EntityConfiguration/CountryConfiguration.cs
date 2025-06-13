using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eStoreCA.Domain.Entities;

namespace eStoreCA.Infrastructure.EntityConfiguration
{
    public partial class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries", "dbo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar(10)").HasMaxLength(10).IsRequired();
            builder.Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit").IsRequired();
            builder.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime2").IsRequired();
            builder.Property(t => t.LastModifiedBy).HasColumnName("LastModifiedBy").HasColumnType("uniqueidentifier");
            builder.Property(t => t.LastModifiedAt).HasColumnName("LastModifiedAt").HasColumnType("datetime2");
            builder.Property(t => t.RowVersion).HasColumnName("RowVersion").IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            builder.Property(t => t.SoftDeleted).HasColumnName("SoftDeleted").HasColumnType("bit").IsRequired();
            builder.Property(t => t.DeletedBy).HasColumnName("DeletedBy").HasColumnType("uniqueidentifier");
            builder.Property(t => t.DeletedAt).HasColumnName("DeletedAt").HasColumnType("datetime2");
        }
    }
}
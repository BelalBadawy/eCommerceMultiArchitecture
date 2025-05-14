
using eStoreCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace eStoreCA.Infrastructure.Data.EntityConfiguration
{
    public partial class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // table
            builder.ToTable("Categories", "dbo");

            // key


            builder.HasKey(t => t.Id); builder.Property(t => t.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.Title).HasColumnName("Title").HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
            builder.Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit").IsRequired();
            builder.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime2").IsRequired();
            builder.Property(t => t.LastModifiedBy).HasColumnName("LastModifiedBy").HasColumnType("uniqueidentifier");
            builder.Property(t => t.LastModifiedAt).HasColumnName("LastModifiedAt").HasColumnType("datetime2");
            builder.Property(t => t.RowVersion).HasColumnName("RowVersion").IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            builder.Property(t => t.SoftDeleted).HasColumnName("SoftDeleted").HasColumnType("bit").IsRequired();
            builder.Property(t => t.DeletedBy).HasColumnName("DeletedBy").HasColumnType("uniqueidentifier");
            builder.Property(t => t.DeletedAt).HasColumnName("DeletedAt").HasColumnType("datetime2");
            
            #region Custom
            #endregion Custom

        }
    }
}

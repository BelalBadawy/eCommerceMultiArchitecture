using System;
using System.ComponentModel.DataAnnotations;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Entities
{
    public class Country : BaseEntity<Guid>, IAuditable, ISoftDelete, IDataConcurrency
    {
        public Country() { }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } // ISO code, e.g., US, IN
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedAt { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool SoftDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedAt { get; set; }
        #region Custom
        #endregion Custom
    }
}
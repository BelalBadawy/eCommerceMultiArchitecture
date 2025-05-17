using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;


namespace eStoreCA.Domain.Entities
{

    public class Category : BaseEntity<Guid>, IAuditable, ISoftDelete, IDataConcurrency
    {

        public Category()
        {

            #region Custom Constructor
            #endregion Custom Constructor

        }



        public Guid Id { get; set; }



        public string Title { get; set; }



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

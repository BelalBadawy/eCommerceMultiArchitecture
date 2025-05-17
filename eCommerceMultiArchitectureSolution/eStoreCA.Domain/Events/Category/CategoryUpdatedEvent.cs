using eStoreCA.Domain.Entities;
using eStoreCA.Shared.Interfaces;

using eStoreCA.Shared.Dtos;
namespace eStoreCA.Domain.Events
{


    public class CategoryUpdatedEvent : IDomainEvent
    {
        public CategoryUpdatedEvent(UpdateCategoryDto category)
        {
            Category = category;
        }

        public UpdateCategoryDto Category { get; }

        #region Custom
        #endregion Custom

    }
}

using eStoreCA.Domain.Entities;
using eStoreCA.Shared.Interfaces;

using eStoreCA.Shared.Dtos;
namespace eStoreCA.Domain.Events
{


    public class CategoryDeletedEvent : IDomainEvent
    {
        public CategoryDeletedEvent(DeleteCategoryDto category)
        {
            Category = category;
        }

        public DeleteCategoryDto Category { get; }

        #region Custom
        #endregion Custom

    }
}

using eStoreCA.Domain.Entities;
using eStoreCA.Shared.Interfaces;

using eStoreCA.Shared.Dtos;
namespace eStoreCA.Domain.Events
{


    public class CategoryCreatedEvent : IDomainEvent
    {
        public CategoryCreatedEvent(CreateCategoryDto category)
        {
            Category = category;
        }

        public CreateCategoryDto Category { get; }

        #region Custom
        #endregion Custom

    }
}

using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events;

public class CategoryDeletedEvent : IDomainEvent
{
    public CategoryDeletedEvent(DeleteCategoryDto category)
    {
        Category = category;
    }

    public DeleteCategoryDto Category { get; }
}
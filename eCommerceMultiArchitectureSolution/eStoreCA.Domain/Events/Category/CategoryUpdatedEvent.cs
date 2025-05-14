using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events;

public class CategoryUpdatedEvent : IDomainEvent
{
    public CategoryUpdatedEvent(UpdateCategoryDto category)
    {
        Category = category;
    }

    public UpdateCategoryDto Category { get; }
}
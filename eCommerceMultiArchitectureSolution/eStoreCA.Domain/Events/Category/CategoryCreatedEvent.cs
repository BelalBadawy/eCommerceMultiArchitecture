using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events;

public class CategoryCreatedEvent : IDomainEvent
{
    public CategoryCreatedEvent(CreateCategoryDto category)
    {
        Category = category;
    }

    public CreateCategoryDto Category { get; }
}
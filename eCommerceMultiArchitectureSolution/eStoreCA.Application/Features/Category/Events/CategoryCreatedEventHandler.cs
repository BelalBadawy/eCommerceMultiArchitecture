using Mediator;
using eStoreCA.Domain.Events;

namespace eStoreCA.Application.Features.Events
{
    public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
    {
        public async ValueTask Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("CreatedEvent: " + notification);
        }
    }
}

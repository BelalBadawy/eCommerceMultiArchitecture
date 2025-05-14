using Mediator;
using eStoreCA.Domain.Events;

namespace eStoreCA.Application.Features.Events
{
    public class CategoryUpdatedEventHandler : INotificationHandler<CategoryUpdatedEvent>
    {
        public async ValueTask Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("UpdatedEvent: " + notification);
        }
    }
}

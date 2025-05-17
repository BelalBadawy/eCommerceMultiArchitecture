
using Mediator;
using eStoreCA.Domain.Events;

namespace eStoreCA.Application.Features.Events

{
 
public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
 {
     public async ValueTask Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
     {
         Console.WriteLine("DeletedEvent: " + notification);
     }
 }
 }

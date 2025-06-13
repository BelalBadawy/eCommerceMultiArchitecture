using Mediator;
using eStoreCA.Domain.Events.Country;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Events
{
    public class CountryCreatedEventHandler : INotificationHandler<CountryCreatedEvent>
    {
        public async ValueTask Handle(CountryCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Country created: " + notification.Country.Name);
        }
    }
}
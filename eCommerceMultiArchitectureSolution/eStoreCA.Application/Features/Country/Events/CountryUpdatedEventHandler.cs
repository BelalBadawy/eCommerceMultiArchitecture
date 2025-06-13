using Mediator;
using eStoreCA.Domain.Events.Country;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Events
{
    public class CountryUpdatedEventHandler : INotificationHandler<CountryUpdatedEvent>
    {
        public async ValueTask Handle(CountryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Country updated: " + notification.Country.Name);
        }
    }
}
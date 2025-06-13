using Mediator;
using eStoreCA.Domain.Events.Country;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Events
{
    public class CountryDeletedEventHandler : INotificationHandler<CountryDeletedEvent>
    {
        public async ValueTask Handle(CountryDeletedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Country deleted: " + notification.Country.Name);
        }
    }
}
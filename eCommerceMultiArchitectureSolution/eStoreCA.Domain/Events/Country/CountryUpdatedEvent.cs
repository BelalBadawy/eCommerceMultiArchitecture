using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events.Country
{
    public class CountryUpdatedEvent : IDomainEvent
    {
        public CountryUpdatedEvent(UpdateCountryDto country)
        {
            Country = country;
        }
        public UpdateCountryDto Country { get; }
    }
}
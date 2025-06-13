using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events.Country
{
    public class CountryCreatedEvent : IDomainEvent
    {
        public CountryCreatedEvent(CreateCountryDto country)
        {
            Country = country;
        }
        public CreateCountryDto Country { get; }
    }
}
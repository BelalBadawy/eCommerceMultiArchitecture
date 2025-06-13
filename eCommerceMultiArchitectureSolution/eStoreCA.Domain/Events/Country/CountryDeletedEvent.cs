using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Domain.Events.Country
{
    public class CountryDeletedEvent : IDomainEvent
    {
        public CountryDeletedEvent(UpdateCountryDto country)
        {
            Country = country;
        }
        public UpdateCountryDto Country { get; }
    }
}
using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Common;
using Mediator;
using eStoreCA.Application.Attributes;

namespace eStoreCA.Application.Features.Country.Commands.Create
{
    [Authorize(Policy = AppPermissions.CountryPermissions.Create)]
    public class CreateCountryCommand : CreateCountryDto, IRequest<MyAppResponse<Guid>>
    {
        // Additional command-specific properties if needed
    }
}
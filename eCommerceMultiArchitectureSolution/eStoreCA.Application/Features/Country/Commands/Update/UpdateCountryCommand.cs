using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Common;
using Mediator;
using eStoreCA.Application.Attributes;

namespace eStoreCA.Application.Features.Country.Commands.Update
{
    [Authorize(Policy = AppPermissions.CountryPermissions.Edit)]
    public class UpdateCountryCommand : UpdateCountryDto, IRequest<MyAppResponse<bool>>
    {
    }
}
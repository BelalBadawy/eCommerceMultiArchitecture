using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Common;
using Mediator;
using System.Collections.Generic;
using eStoreCA.Application.Attributes;

namespace eStoreCA.Application.Features.Country.Queries.GetAll
{
    [Authorize(Policy = AppPermissions.CountryPermissions.View)]
    public class GetAllCountryQuery : IRequest<MyAppResponse<List<GetCountryDto>>>
    {
        public string Search { get; set; }
        public string SortColumnName { get; set; }
        public bool AscendingOrder { get; set; }
        public bool BypassCache { get; set; } = false;
    }
}
using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Common;
using Mediator;
using eStoreCA.Application.Attributes;

namespace eStoreCA.Application.Features.Country.Queries.GetAllByPage
{
    [Authorize(Policy = AppPermissions.CountryPermissions.View)]
    public class GetAllByPageCountryQuery : GetAllByPageCountryDto, IRequest<MyAppResponse<PagedResult<GetAllByPageCountryDto>>>
    {
        public string Search { get; set; }
        public string SortColumnName { get; set; }
        public bool AscendingOrder { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool BypassCache { get; set; } = false;
    }
}
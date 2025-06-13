using eStoreCA.Shared.Dtos.Country;
using eStoreCA.Shared.Common;
using Mediator;
using System;
using eStoreCA.Application.Attributes;

namespace eStoreCA.Application.Features.Country.Queries.GetById
{
    [Authorize(Policy = AppPermissions.CountryPermissions.View)]
    public class GetByIdCountryQuery : IRequest<MyAppResponse<GetCountryDto>>
    {
        public Guid Id { get; set; }
    }
}
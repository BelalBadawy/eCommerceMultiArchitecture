using eStoreCA.Shared.Common;
using Mediator;
using eStoreCA.Application.Attributes;
using System;

namespace eStoreCA.Application.Features.Country.Commands.Delete
{
    [Authorize(Policy = AppPermissions.CountryPermissions.Delete)]
    public class DeleteCountryCommand : IRequest<MyAppResponse<bool>>
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
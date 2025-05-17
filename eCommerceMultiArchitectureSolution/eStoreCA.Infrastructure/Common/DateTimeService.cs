
using System;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Infrastructure.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;

        #region Custom
        #endregion Custom

    }
}

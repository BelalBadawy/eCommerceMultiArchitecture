
namespace eStoreCA.Shared.Interfaces
{
    public interface ICacheAbleMediatorQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }



        #region Custom
        #endregion Custom


    }
}

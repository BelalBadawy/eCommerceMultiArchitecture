
using Mediator;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace eStoreCA.Application.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheAbleMediatorQuery
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly CacheConfiguration _settings;

        public CachingBehaviour(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheConfiguration> settings)
        {
            _cache = cache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            if (request.BypassCache)
                return await next(request, cancellationToken);

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next(request, cancellationToken);
                var slidingExpiration = request.SlidingExpiration == null ? TimeSpan.FromHours(_settings.SlidingExpirationInMinutes) : request.SlidingExpiration;
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await _cache.SetAsync((string)request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }
            var cachedResponse = await _cache.GetAsync((string)request.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
            }

            return response;
        }
    }
}


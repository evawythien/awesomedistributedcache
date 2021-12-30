using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Caching
{
    public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableRequest
    {
        private readonly IDistributedCache cache;
        private readonly ILogger<CachePipelineBehavior<TRequest, TResponse>> logger;

        public CachePipelineBehavior(IDistributedCache cache, ILogger<CachePipelineBehavior<TRequest, TResponse>> logger)
        {
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var key = request.GetCacheKey();
            byte[]? value = await cache.GetAsync(key, cancellationToken);

            if (value != null)
            {
                return CompressionUtilities.Unzip<TResponse>(value);
            }

            TResponse response = await next();

            await this.cache.SetAsync(key, CompressionUtilities.Zip(response), new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }
    }
}

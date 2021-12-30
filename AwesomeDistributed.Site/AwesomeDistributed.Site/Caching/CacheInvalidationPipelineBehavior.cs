using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeDistributed.Site.Caching
{
    public class CacheInvalidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheInvalidationRequest
    {
        private readonly IDistributedCache cache;
        private readonly ConnectionMultiplexer redis;
        private readonly ILogger<CacheInvalidationPipelineBehavior<TRequest, TResponse>> logger;

        public CacheInvalidationPipelineBehavior(IDistributedCache cache, ILogger<CacheInvalidationPipelineBehavior<TRequest, TResponse>> logger, ConnectionMultiplexer redis)
        {
            this.cache = cache;
            this.logger = logger;
            this.redis = redis;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            EndPoint[] endpoints = redis.GetEndPoints();
            RedisKey[] keys = endpoints
                .SelectMany(e => redis.GetServer(e).Keys(pattern: request.GetCacheKey() + "*"))
                .Distinct().ToArray();

            await redis.GetDatabase().KeyDeleteAsync(keys);

            return response;
        }
    }
}


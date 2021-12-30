namespace AwesomeDistributed.Site.Caching
{
    public interface ICacheInvalidationRequest
    {
        public string GetCacheKey();
    }
}

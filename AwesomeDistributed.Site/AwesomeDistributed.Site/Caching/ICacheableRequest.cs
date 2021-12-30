namespace AwesomeDistributed.Site.Caching
{
    public interface ICacheableRequest
    {
        public string GetCacheKey();
    }
}

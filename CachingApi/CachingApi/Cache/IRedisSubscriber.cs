namespace CachingApi.Cache;

public interface IRedisSubscriber
{
    Task OnItemRemoved(string key);
}

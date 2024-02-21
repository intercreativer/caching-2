using StackExchange.Redis;

namespace CachingApi;

public class RedisDataRepository : RedisGenericCache, IDataRepository
{
    readonly IDataRepository repository;
    public RedisDataRepository(IDataRepository repository, ConnectionMultiplexer redis) : base(redis)
    {
        this.repository = repository;
    }
    public async Task<IList<DataEntity>> SearchEntities(string name)
    {
        return await GetOrSet($"SearchEntities_{name}", () => repository.SearchEntities(name));
    }

    public async Task Add(string name)
    {
        await RemoveItem($"SearchEntities_{name}");
        await repository.Add(name);
    }
}
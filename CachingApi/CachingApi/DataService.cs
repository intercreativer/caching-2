using CachingApi.Repository;

namespace CachingApi;

public interface IDataService
{
    Task<IList<DataEntity>> SearchEntities(string name);
    Task Add(string name);
}


public class DataService : IDataService
{
    readonly IDataRepository repository;
    public DataService(IDataRepository repository)
    {
        this.repository = repository;
    }

    public async Task Add(string name)
    {
        await repository.Add(name);
    }

    public async Task<IList<DataEntity>> SearchEntities(string name)
    {
        return await repository.SearchEntities(name);
    }
}

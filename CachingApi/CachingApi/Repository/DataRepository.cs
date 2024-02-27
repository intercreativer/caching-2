namespace CachingApi.Repository;

public interface IDataRepository
{
    Task<IList<DataEntity>> SearchEntities(string name);
    Task Add(string name);
}

public class DataRepository : IDataRepository
{
    public async Task Add(string name)
    {

    }

    public async Task<IList<DataEntity>> SearchEntities(string name)
    {
        await Task.Delay(1500);
        return GenFu.GenFu.ListOf<DataEntity>();
    }
}

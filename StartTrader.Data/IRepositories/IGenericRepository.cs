namespace StartTrader.Data.IRepositories;
public interface IGenericRepository<TResult>
{
    Task<TResult> CreateAsync(TResult model);
    Task<TResult> UpdateAsync(TResult model);
    Task<bool> DeleteAsync(long id);
    Task<TResult> GetByIdAsync(long id);
    Task<List<TResult>> GetAllAsync();
}

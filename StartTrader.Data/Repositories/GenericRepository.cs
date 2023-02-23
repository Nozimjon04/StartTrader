using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StartTrader.Data.Configurations;
using StartTrader.Data.IRepositories;
using StartTrader.Domain.Commons;
using StartTrader.Domain.Entities;
using System.IO;

namespace StartTrader.Data.Repositories;
public class GenericRepository<TResult> : IGenericRepository<TResult> where TResult : Auditable
{
    private string Path;
    private long LastId = 0;
    public GenericRepository()
    {
        StartUp();
    }
    public async void StartUp()
    {
        if (typeof(TResult) == typeof(Communication))
        {
            Path = DatabasePaths.COMMUNICATION_PATH;
        }
        else if (typeof(TResult) == typeof(Course))
        {
            Path = DatabasePaths.COURSE_PATH;
        }
        else if (typeof(TResult) == typeof(Payment))
        {
            Path = DatabasePaths.PAYMENT_PATH;
        }
        else if (typeof(TResult) == typeof(User))
        {
            Path = DatabasePaths.USER_PATH;
        }
        var res=await GetAllAsync();
        if (res is not null)
        {

            foreach (var model in res)
            {
                if (model.ID > LastId)
                    LastId = model.ID;
            }
        }

    }
    public async Task<TResult> CreateAsync(TResult value)
    {
        value.ID = ++LastId;
        value.CreateAt = DateTime.UtcNow;

        var values = await GetAllAsync();
        values.Add(value);

        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(Path, json);
        return value;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var values = await GetAllAsync();
        var value = values.FirstOrDefault(x => x.ID == id);

        if (value is null)
            return false;

        values.Remove(value);
        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(Path, json);

        return true;
    }

    public async Task<List<TResult>> GetAllAsync()
    {
        var models = await File.ReadAllTextAsync(Path);
        if (models.Length==0)
        {
            models = "[]";
        }
        

       var results = JsonConvert.DeserializeObject<List<TResult>>(models);
        return results;
    }

    public async Task<TResult> GetByIdAsync(long id)
    {
        var values = await GetAllAsync();
        return values.FirstOrDefault(x => x.ID == id);
    }

    public async Task<TResult> UpdateAsync(TResult value)
    {
        var values = await GetAllAsync();
        var model = values.FirstOrDefault(x => x.ID == value.ID);
        if (model is not null)
        {
            var index = values.IndexOf(model);
            values.Remove(model);

            value.CreateAt = model.CreateAt;
            value.UpdateAt = DateTime.UtcNow;

            values.Insert(index, value);
            var json = JsonConvert.SerializeObject(values, Formatting.Indented);
            await File.WriteAllTextAsync(Path, json);
            return model;
        }

        return model;
    }

   
    
}

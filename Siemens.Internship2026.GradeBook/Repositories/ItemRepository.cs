using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemReader
{
    //protected readonly List<Item> _items = new();
    private readonly HttpClient _httpClient;
    //protected int _nextId = 1;

    public ItemRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Item>> GetAllAsync() 
    { 
        var items = await _httpClient.GetFromJsonAsync<List<Item>>("hps://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1");
        return items ?? Enumerable.Empty<Item>();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var items = await GetAllAsync();
        return  items.FirstOrDefault(i => i.Id == id && i.IsActive);
       
    }

    //public  Task<Item?> GetByIdAsync(int id)
    //{
    //    var item = _items.FirstOrDefault(i => i.Id == id && i.IsActive);
    //    return Task.FromResult(item);
    //}

    //public virtual Task<IEnumerable<Item>> GetAllAsync()
    //{
    //    var items = _items.Where(i => i.IsActive).AsEnumerable();
    //    return Task.FromResult(items);
    //}
}

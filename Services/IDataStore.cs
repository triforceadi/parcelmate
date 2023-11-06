using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace parcelmate.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T courier);
        Task<bool> UpdateItemAsync(T courier);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

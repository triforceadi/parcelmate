using parcelmate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parcelmate.Services
{
    public class MockDataStore : IDataStore<Courier>
    {
        public List<Courier> couriers;

        public MockDataStore()
        {
            couriers = new List<Courier>();
        }

        public async Task<bool> AddItemAsync(Courier courier)
        {
            couriers.Add(courier);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Courier item)
        {
            var oldItem = couriers.Where((Courier arg) => arg.id == item.id).FirstOrDefault();
            couriers.Remove(oldItem);
            couriers.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = couriers.Where((Courier arg) => arg.id == id).FirstOrDefault();
            couriers.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Courier> GetItemAsync(string id)
        {
            return await Task.FromResult(couriers.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Courier>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(couriers);
        }
    }
}
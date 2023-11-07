using parcelmate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parcelmate.Services
{
    public class CourierDataService
    {
        private readonly MockDataStore _mockDataStore;

        public CourierDataService()
        {
            _mockDataStore = new MockDataStore();
        }

        public async Task<bool> SaveCourierAsync(Courier courier)
        {
            try
            {
                await _mockDataStore.AddItemAsync(courier);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Courier GetLastSavedCourier()
        {
            IEnumerable<Courier> couriers = _mockDataStore.GetItemsAsync().Result;


            return couriers?.LastOrDefault();
        }
    }
}

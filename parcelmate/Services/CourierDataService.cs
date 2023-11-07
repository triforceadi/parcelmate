using Newtonsoft.Json;
using parcelmate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parcelmate.Services
{
    public class CourierDataService
    {
        private DataModel couriers;
        public CourierDataService()
        {
        }
        public DataModel InitializeData()
        {
            try
            {
                couriers = JsonConvert.DeserializeObject<DataModel>("{\n  \"couriers\": [\n    {\n      \"id\": 1,\n      \"firstName\": \"Adrian\",\n      \"surname\":\"Trifoi\",\n      \"age\": \"28\",\n      \"driverLicenseExpiryDate\":\"12-12-2030\",\n      \"driverLicenseCategory\":\"B\",\n      \"isCertified\": \"false\",\n      \"isAllowedDangerousGoods\": \"false\"\n    },\n    {\n      \"id\": 2,\n      \"firstName\": \"Mihai\",\n      \"surname\":\"Tomescu\",\n      \"age\": \"35\",\n      \"driverLicenseExpiryDate\":\"12-12-2027\",\n      \"driverLicenseCategory\":\"B, C, D\",\n      \"isCertified\": \"false\",\n      \"isAllowedDangerousGoods\": \"true\"\n    },\n    {\n      \"id\": 3,\n      \"firstName\": \"Dan\",\n      \"surname\":\"Baghilovici\",\n      \"age\": \"28\",\n      \"driverLicenseExpiryDate\":\"12-12-2024\",\n      \"driverLicenseCategory\":\"B, C\",\n      \"isCertified\": \"false\",\n      \"isAllowedDangerousGoods\": \"false\"\n    }\n  ]\n}");
            }
            catch(Exception ex)
            {
            }

            return couriers;
        }

        public Courier GetCourierById(int courierId)
        {
            if (couriers != null && couriers.Couriers != null)
            {
                return couriers.Couriers.Find(courier => courier.id == courierId);
            }

            return null;
        }
    }
}

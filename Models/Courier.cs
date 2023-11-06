using System;

namespace parcelmate.Models
{
    public class Courier
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string age { get; set; }
        public string driverLicenseExpiryDate { get; set; }
        public string driverLicenseCategory { get; set; }
        public string isCertified { get; set; }
        public string isAllowedDangerousGoods { get; set; }
    }
}
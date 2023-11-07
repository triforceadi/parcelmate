using System;
using System.Collections.Generic;
using System.Text;

namespace parcelmate.Services
{
    public class BarcodeService
    {
        private List<string> validBarcodes;

        public BarcodeService()
        {
            validBarcodes = new List<string>
            {
                "123456",
                "789012",
                "123-456-789"
            };
        }

        public bool VerifyBarcode(string scannedBarcode)
        {
            return validBarcodes.Contains(scannedBarcode);
        }
    }
}
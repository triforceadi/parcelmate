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
                "AWB8777633",
                "AWB8777612",
                "AWB125099",
                "AWB123988",
                "AWB123999",
                "AWB1223399",
                "AWB123399",
                "AWB1523699",
                "AWB1623499",
                "AWB123999", 
                "AWB12342399",
                "AWB16453499",
                "AWB16435399",
                "AWB1223499",
            };
        }

        public bool VerifyBarcode(string scannedBarcode)
        {
            return validBarcodes.Contains(scannedBarcode);
        }

        public List<string> AvailableBarcodes()
        {
            return validBarcodes;
        }
    }
}
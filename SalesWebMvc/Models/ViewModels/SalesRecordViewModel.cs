using System.Collections;
using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SalesRecordViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public List<Seller> Sellers { get; set; }

        public SalesRecordViewModel(SalesRecord salesRecord, List<Seller> sellers)
        {
            SalesRecord = salesRecord;
            Sellers = sellers;
        }

        public SalesRecordViewModel(List<Seller> sellers)
        {
            Sellers = sellers;
        }
    }
}

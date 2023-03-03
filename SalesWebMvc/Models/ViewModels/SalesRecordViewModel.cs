using System.Collections;
using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SalesRecordViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public List<Seller> Sellers { get; set; }
    }
}

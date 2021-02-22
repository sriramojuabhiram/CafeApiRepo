using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeApi
{
    public class BillInfo
    {

        public IEnumerable<MenuItem> MenuItems { get; set;  }
        public decimal TotalPrice { get; set; }
        public decimal ServiceCharge { get; set; } = 0.1M;
    }
}

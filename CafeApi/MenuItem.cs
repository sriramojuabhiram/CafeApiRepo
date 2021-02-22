using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeApi
{
    public class MenuItem
    {
         
        public string Name { get; set; }

        public int Quantity { get; set; }

        public bool IsDrink { get; set; }

        public decimal UnitPrice { get; set; }
    }


}

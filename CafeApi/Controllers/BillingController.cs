using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CafeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;

        public BillingController(ILogger<BillingController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// calcualtes the bill based on The menu items sent in the list.
        /// </summary>
        /// <param name="menuItems">Menu Items with Quantity and Name</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CalculateBill([FromBody] IEnumerable<MenuItem> menuItems)
        {
            BillInfo bill = new BillInfo();
            try
            {
                var billItems = SetUnitPrice(menuItems);
                
                foreach (var item in billItems)
                {
                    bill.TotalPrice += (item.Quantity * item.UnitPrice);
                }
                if (billItems.Any(x => !x.IsDrink))
                {
                    bill.TotalPrice = (bill.TotalPrice * bill.ServiceCharge);
                }
                bill.TotalPrice = Math.Round(bill.TotalPrice, 2);
                return Ok(bill);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        /// <summary>
        /// Sets Unit price based on the name of the Item.
        /// </summary>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        private IEnumerable<MenuItem> SetUnitPrice(IEnumerable<MenuItem> menuItems)
        {
            foreach (var item in menuItems)
            {
                switch (item.Name.ToUpper())
                {
                    case "COFFEE":
                        item.IsDrink = true;
                        item.UnitPrice = 0.50M;
                        break;
                    case "COLA":
                        item.IsDrink = true;
                        item.UnitPrice = 1.00M;
                        break;
                    case "CHEESE SANDWICH ":
                        item.IsDrink = false;
                        item.UnitPrice = 2.00M;
                        break;
                    case "STEAK SANDWICH":
                        item.IsDrink = false;
                        item.UnitPrice = 4.50M;
                        break;
                }
            }


            return menuItems;
        }
    }
}

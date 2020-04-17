using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPSInventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPSInventory.Controllers
{
    public class StoreInventoryController : Controller
    {

        public async Task<IActionResult> Index(int id = 0, int Idemployee = 0, int storeId = 0)
        {
            InventoryContext _context = new InventoryContext();

            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);

            }
            else
            {
                TempData["message"] = "You must login to view that page";
                return Redirect("/Home");
            }
            if (id != 0)
            {
                var productContext = _context.InItemOrder.Include(a => a.IdProductNavigation).Where(a => a.IdInorder == id);

                return View(await productContext.ToListAsync());
            }

            var recOrders = _context.OutOrder.Where(a => a.IdStore == storeId);

            var sales = _context.CustomerOrder.Where(a => a.IdStore == storeId);

            List<OutItemOrder> lineItems = new List<OutItemOrder>();

            Boolean first = true;

            foreach (var order in recOrders)
            {
                var items = _context.OutItemOrder.Include(a=>a.IdProductNavigation).Where(a => a.IdOutorder == order.IdoutOrder);

                foreach (var item in items)
                {
                    if (first)
                    {
                        lineItems.Add(item);
                        first = false;
                    }
                    else
                    {
                        for (int i = 0; i < lineItems.Count; i++)
                        {
                            if (lineItems[i].IdProduct == item.IdProduct)
                            {
                                lineItems[i].Quantity += item.Quantity;
                                break;
                            }
                            else if (i+1 == lineItems.Count)
                            {
                                lineItems.Add(item);
                                break;
                            }
                        }
                    }
                }
            }

            foreach (var sale in sales)
            {
                var items = _context.CustomerItem.Include(a => a.IdProductNavigation).Where(a => a.IdCustomerOrder == sale.IdcustomerOrder);

                foreach (var item in items)
                {
                    for (int i = 0; i < lineItems.Count; i++)
                    {
                        if (lineItems[i].IdProduct == item.IdProduct)
                        {
                            lineItems[i].Quantity -= item.Quantity;
                        }
                    }
                }
            }

            return View(lineItems);
        }
    }
}
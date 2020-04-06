using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPSInventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FPSInventory.Controllers
{
    public class ReportController : Controller
    {
        InventoryContext _context = new InventoryContext();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Stores()
        {
            InventoryContext _context = new InventoryContext();
            var inventoryContext = _context.Store.Include(s => s.IdCityNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        public IActionResult Sales()
        {
            ViewData["Idstore"] = new SelectList(_context.Store, "Idstore", "Namestore");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sales(int Idstore, DateTime fromDate, DateTime toDate)
        {

            HttpContext.Session.SetString("searchStoreId", Idstore.ToString());
            HttpContext.Session.SetString("searchFromDate", fromDate.ToString());
            HttpContext.Session.SetString("searchToDate", toDate.ToString());

            return Redirect("/CustomerOrder");

            var sales = _context.CustomerOrder
                                .Where(a => a.IdStore == Idstore)
                                .Where(a => a.Date > fromDate)
                                .Where(a => a.Date < toDate);

            if (sales != null)
            {
                return RedirectToAction("SalesByDate",sales.ToListAsync());
            }
            ViewData["Idstore"] = new SelectList(_context.Store, "Idstore", "Namestore");
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPSInventory.Models;
using Microsoft.AspNetCore.Http;

namespace FPSInventory.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly InventoryContext _context;

        public CustomerOrderController(InventoryContext context)
        {
            _context = context;
        }

        // GET: CustomerOrder
        public async Task<IActionResult> Index(int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);

                
            }
            else
            {
                TempData["message"] = "You must login to view the Sales page";
                return Redirect("/Home");
            }

            if (HttpContext.Session.GetString("searchStoreId") != null)
            {
                int storeId = int.Parse(HttpContext.Session.GetString("searchStoreId"));
                DateTime fromDate = DateTime.Parse(HttpContext.Session.GetString("searchFromDate"));
                DateTime toDate = DateTime.Parse(HttpContext.Session.GetString("searchToDate"));

                HttpContext.Session.Remove("searchStoreId");
                HttpContext.Session.Remove("searchFromDate");
                HttpContext.Session.Remove("searchToDate");

                var sales = _context.CustomerOrder
                                .Include(c => c.IdStoreNavigation)
                                .Where(a => a.IdStore == storeId)
                                .Where(a => a.Date > fromDate)
                                .Where(a => a.Date < toDate);
                return View(await sales.ToListAsync());
            }

            var inventoryContext = _context.CustomerOrder.Include(c => c.IdStoreNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: CustomerOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder
                .Include(c => c.IdStoreNavigation)
                .FirstOrDefaultAsync(m => m.IdcustomerOrder == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // GET: CustomerOrder/Create
        public IActionResult Create()
        {
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Namestore");
            return View();
        }

        // POST: CustomerOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcustomerOrder,Date,IdStore")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Namestore", customerOrder.IdStore);
            return View(customerOrder);
        }

        // GET: CustomerOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Namestore", customerOrder.IdStore);
            return View(customerOrder);
        }

        // POST: CustomerOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdcustomerOrder,Date,IdStore")] CustomerOrder customerOrder)
        {
            if (id != customerOrder.IdcustomerOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderExists(customerOrder.IdcustomerOrder))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Namestore", customerOrder.IdStore);
            return View(customerOrder);
        }

        // GET: CustomerOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder
                .Include(c => c.IdStoreNavigation)
                .FirstOrDefaultAsync(m => m.IdcustomerOrder == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // POST: CustomerOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            _context.CustomerOrder.Remove(customerOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrder.Any(e => e.IdcustomerOrder == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPSInventory.Models;

namespace FPSInventory.Controllers
{
    public class CustomerItemController : Controller
    {
        private readonly InventoryContext _context;

        public CustomerItemController(InventoryContext context)
        {
            _context = context;
        }

        // GET: CustomerItem
        public async Task<IActionResult> Index(int id = 0)
        {
            if (id != 0)
            {
                var productContext = _context.CustomerItem
                    .Include(c => c.IdCustomerOrderNavigation)
                    .Include(c => c.IdProductNavigation)
                    .Where(c => c.IdCustomerOrder == id);
                return View(await productContext.ToListAsync());

            }
            var inventoryContext = _context.CustomerItem.Include(c => c.IdCustomerOrderNavigation).Include(c => c.IdProductNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: CustomerItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItem
                .Include(c => c.IdCustomerOrderNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdoutItemOrder == id);
            if (customerItem == null)
            {
                return NotFound();
            }

            return View(customerItem);
        }

        // GET: CustomerItem/Create
        public IActionResult Create()
        {
            ViewData["IdCustomerOrder"] = new SelectList(_context.CustomerOrder, "IdcustomerOrder", "IdcustomerOrder");
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1");
            return View();
        }

        // POST: CustomerItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdoutItemOrder,IdProduct,IdCustomerOrder,Quantity,Price")] CustomerItem customerItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomerOrder"] = new SelectList(_context.CustomerOrder, "IdcustomerOrder", "IdcustomerOrder", customerItem.IdCustomerOrder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", customerItem.IdProduct);
            return View(customerItem);
        }

        // GET: CustomerItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItem.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }
            ViewData["IdCustomerOrder"] = new SelectList(_context.CustomerOrder, "IdcustomerOrder", "IdcustomerOrder", customerItem.IdCustomerOrder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", customerItem.IdProduct);
            return View(customerItem);
        }

        // POST: CustomerItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdoutItemOrder,IdProduct,IdCustomerOrder,Quantity,Price")] CustomerItem customerItem)
        {
            if (id != customerItem.IdoutItemOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerItemExists(customerItem.IdoutItemOrder))
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
            ViewData["IdCustomerOrder"] = new SelectList(_context.CustomerOrder, "IdcustomerOrder", "IdcustomerOrder", customerItem.IdCustomerOrder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", customerItem.IdProduct);
            return View(customerItem);
        }

        // GET: CustomerItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItem
                .Include(c => c.IdCustomerOrderNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdoutItemOrder == id);
            if (customerItem == null)
            {
                return NotFound();
            }

            return View(customerItem);
        }

        // POST: CustomerItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerItem = await _context.CustomerItem.FindAsync(id);
            _context.CustomerItem.Remove(customerItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerItemExists(int id)
        {
            return _context.CustomerItem.Any(e => e.IdoutItemOrder == id);
        }
    }
}

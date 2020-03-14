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
    public class OutOrderController : Controller
    {
        private readonly InventoryContext _context;

        public OutOrderController(InventoryContext context)
        {
            _context = context;
        }

        // GET: OutOrder
        public async Task<IActionResult> Index()
        {
            var inventoryContext = _context.OutOrder.Include(o => o.IdEmployeeNavigation).Include(o => o.IdShippingCompanyNavigation).Include(o => o.IdStoreNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: OutOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outOrder = await _context.OutOrder
                .Include(o => o.IdEmployeeNavigation)
                .Include(o => o.IdShippingCompanyNavigation)
                .Include(o => o.IdStoreNavigation)
                .FirstOrDefaultAsync(m => m.IdoutOrder == id);
            if (outOrder == null)
            {
                return NotFound();
            }

            return View(outOrder);
        }

        // GET: OutOrder/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "name");
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany");
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Idstore");
            return View();
        }

        // POST: OutOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdoutOrder,IdShippingCompany,IdStore,IdEmployee,Date")] OutOrder outOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "name", outOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany", outOrder.IdShippingCompany);
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Idstore", outOrder.IdStore);
            return View(outOrder);
        }

        // GET: OutOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outOrder = await _context.OutOrder.FindAsync(id);
            if (outOrder == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "name", outOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany", outOrder.IdShippingCompany);
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Idstore", outOrder.IdStore);
            return View(outOrder);
        }

        // POST: OutOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdoutOrder,IdShippingCompany,IdStore,IdEmployee,Date")] OutOrder outOrder)
        {
            if (id != outOrder.IdoutOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutOrderExists(outOrder.IdoutOrder))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "name", outOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany", outOrder.IdShippingCompany);
            ViewData["IdStore"] = new SelectList(_context.Store, "Idstore", "Idstore", outOrder.IdStore);
            return View(outOrder);
        }

        // GET: OutOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outOrder = await _context.OutOrder
                .Include(o => o.IdEmployeeNavigation)
                .Include(o => o.IdShippingCompanyNavigation)
                .Include(o => o.IdStoreNavigation)
                .FirstOrDefaultAsync(m => m.IdoutOrder == id);
            if (outOrder == null)
            {
                return NotFound();
            }

            return View(outOrder);
        }

        // POST: OutOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outOrder = await _context.OutOrder.FindAsync(id);
            _context.OutOrder.Remove(outOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutOrderExists(int id)
        {
            return _context.OutOrder.Any(e => e.IdoutOrder == id);
        }
    }
}

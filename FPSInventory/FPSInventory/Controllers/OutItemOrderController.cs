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
    public class OutItemOrderController : Controller
    {
        private readonly InventoryContext _context;

        public OutItemOrderController(InventoryContext context)
        {
            _context = context;
        }

        // GET: OutItemOrder
        public async Task<IActionResult> Index(int Idemployee = 0)
        {
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

            var inventoryContext = _context.OutItemOrder.Include(o => o.IdOutorderNavigation).Include(o => o.IdProductNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: OutItemOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outItemOrder = await _context.OutItemOrder
                .Include(o => o.IdOutorderNavigation)
                .Include(o => o.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdoutItemOrder == id);
            if (outItemOrder == null)
            {
                return NotFound();
            }

            return View(outItemOrder);
        }

        // GET: OutItemOrder/Create
        public IActionResult Create()
        {
            ViewData["IdOutorder"] = new SelectList(_context.OutOrder, "IdoutOrder", "IdoutOrder");
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1");
            return View();
        }

        // POST: OutItemOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdoutItemOrder,IdProduct,IdOutorder,Quantity,Price")] OutItemOrder outItemOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outItemOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOutorder"] = new SelectList(_context.OutOrder, "IdoutOrder", "IdoutOrder", outItemOrder.IdOutorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", outItemOrder.IdProduct);
            return View(outItemOrder);
        }

        // GET: OutItemOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outItemOrder = await _context.OutItemOrder.FindAsync(id);
            if (outItemOrder == null)
            {
                return NotFound();
            }
            ViewData["IdOutorder"] = new SelectList(_context.OutOrder, "IdoutOrder", "IdoutOrder", outItemOrder.IdOutorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", outItemOrder.IdProduct);
            return View(outItemOrder);
        }

        // POST: OutItemOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdoutItemOrder,IdProduct,IdOutorder,Quantity,Price")] OutItemOrder outItemOrder)
        {
            if (id != outItemOrder.IdoutItemOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outItemOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutItemOrderExists(outItemOrder.IdoutItemOrder))
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
            ViewData["IdOutorder"] = new SelectList(_context.OutOrder, "IdoutOrder", "IdoutOrder", outItemOrder.IdOutorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Product1", outItemOrder.IdProduct);
            return View(outItemOrder);
        }

        // GET: OutItemOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outItemOrder = await _context.OutItemOrder
                .Include(o => o.IdOutorderNavigation)
                .Include(o => o.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdoutItemOrder == id);
            if (outItemOrder == null)
            {
                return NotFound();
            }

            return View(outItemOrder);
        }

        // POST: OutItemOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outItemOrder = await _context.OutItemOrder.FindAsync(id);
            _context.OutItemOrder.Remove(outItemOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutItemOrderExists(int id)
        {
            return _context.OutItemOrder.Any(e => e.IdoutItemOrder == id);
        }
    }
}

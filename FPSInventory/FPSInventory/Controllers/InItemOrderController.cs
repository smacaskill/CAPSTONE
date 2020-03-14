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
    public class InItemOrderController : Controller
    {
        private readonly InventoryContext _context;

        public InItemOrderController(InventoryContext context)
        {
            _context = context;
        }

        // GET: InItemOrder
        public async Task<IActionResult> Index(int id = 0)
        {
            if (id != 0)
            {
                var productContext = _context.InItemOrder.Include(a => a.IdProductNavigation).Where(a => a.IdInorder == id);

                return View(await productContext.ToListAsync());
            }

            var inventoryContext = _context.InItemOrder.Include(i => i.IdInorderNavigation).Include(i => i.IdProductNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: InItemOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inItemOrder = await _context.InItemOrder
                .Include(i => i.IdInorderNavigation)
                .Include(i => i.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdinItemOrder == id);
            if (inItemOrder == null)
            {
                return NotFound();
            }

            return View(inItemOrder);
        }

        // GET: InItemOrder/Create
        public IActionResult Create()
        {
            ViewData["IdInorder"] = new SelectList(_context.InOrder, "IdinOrder", "IdinOrder");
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Idproduct");
            return View();
        }

        // POST: InItemOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdinItemOrder,IdProduct,IdInorder,Quantity,Price")] InItemOrder inItemOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inItemOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInorder"] = new SelectList(_context.InOrder, "IdinOrder", "IdinOrder", inItemOrder.IdInorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Idproduct", inItemOrder.IdProduct);
            return View(inItemOrder);
        }

        // GET: InItemOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inItemOrder = await _context.InItemOrder.FindAsync(id);
            if (inItemOrder == null)
            {
                return NotFound();
            }
            ViewData["IdInorder"] = new SelectList(_context.InOrder, "IdinOrder", "IdinOrder", inItemOrder.IdInorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Idproduct", inItemOrder.IdProduct);
            return View(inItemOrder);
        }

        // POST: InItemOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdinItemOrder,IdProduct,IdInorder,Quantity,Price")] InItemOrder inItemOrder)
        {
            if (id != inItemOrder.IdinItemOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inItemOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InItemOrderExists(inItemOrder.IdinItemOrder))
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
            ViewData["IdInorder"] = new SelectList(_context.InOrder, "IdinOrder", "IdinOrder", inItemOrder.IdInorder);
            ViewData["IdProduct"] = new SelectList(_context.Product, "Idproduct", "Idproduct", inItemOrder.IdProduct);
            return View(inItemOrder);
        }

        // GET: InItemOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inItemOrder = await _context.InItemOrder
                .Include(i => i.IdInorderNavigation)
                .Include(i => i.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdinItemOrder == id);
            if (inItemOrder == null)
            {
                return NotFound();
            }

            return View(inItemOrder);
        }

        // POST: InItemOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inItemOrder = await _context.InItemOrder.FindAsync(id);
            _context.InItemOrder.Remove(inItemOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InItemOrderExists(int id)
        {
            return _context.InItemOrder.Any(e => e.IdinItemOrder == id);
        }
    }
}

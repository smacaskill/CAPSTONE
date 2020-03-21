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
    public class ProductController : Controller
    {
        private readonly InventoryContext _context;

        public ProductController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(int categoryId = 0, int storeId = 0)
        {
            if (categoryId != 0)
            {
                var productContext = _context.Product.Where(a => a.IdCategory == categoryId);

                return View(await productContext.ToListAsync());
            }
            //else if (storeId != 0)
            //{
            //    var inOrders = _context.OutOrder.Where(a => a.IdStore == storeId);

            //    int storeQuantity = 0;

            //    foreach (var order in inOrders)
            //    {
            //        var lineItems = _context.OutItemOrder
            //            .Where(a => a.IdOutorder == order.IdoutOrder)

            //        foreach (var item in lineItems)
            //        {
            //            storeQuantity += item.Quantity;
            //            averagePrice += item.Price * item.Quantity;
            //        }
            //    }


            //    var productContext = _context.Product.Where(a => a.IdCategory == categoryId);

            //    return View(await productContext.ToListAsync());
            //}

            var inventoryContext = _context.Product.Include(p => p.IdCategoryNavigation);
            
            return View(await inventoryContext.ToListAsync());

        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.Idproduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Category, "Idcategory", "Namecategory");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproduct,Product1,IdCategory")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "Idcategory", "Namecategory", product.IdCategory);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "Idcategory", "Namecategory", product.IdCategory);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproduct,Product1,IdCategory")] Product product)
        {
            if (id != product.Idproduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Idproduct))
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
            ViewData["IdCategory"] = new SelectList(_context.Category, "Idcategory", "Namecategory", product.IdCategory);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.Idproduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Idproduct == id);
        }
    }
}

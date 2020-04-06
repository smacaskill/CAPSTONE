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
    public class SupplierController : Controller
    {
        private readonly InventoryContext _context;

        public SupplierController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Supplier
        public async Task<IActionResult> Index(int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") == null)
            {
                
                    TempData["message"] = "You must login to access the Supplier page";
                    return Redirect("/Home");
                
            }

            var inventoryContext = _context.Supplier.Include(s => s.IdCityNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .Include(s => s.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.Idsupplier == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create(int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);


                if (employee.Role == "USER")
                {
                    TempData["message"] = "You are not authorized to Add a new Supplier";
                    return Redirect("/Home");
                }
            }

            ViewData["IdCity"] = _context.City
                .Select(c => new SelectListItem
                {
                    Text = c.Namecity + ", " + c.Province,
                    Value = c.Idcity.ToString()
                });
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsupplier,Namesupplier,IdCity,Address")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idsupplier,Namesupplier,IdCity,Address")] Supplier supplier)
        {
            if (id != supplier.Idsupplier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Idsupplier))
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
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id, int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);


                if (employee.Role == "USER")
                {
                    TempData["message"] = "You are not authorized to Delete a Supplier";
                    return Redirect("/Home");
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .Include(s => s.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.Idsupplier == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.Idsupplier == id);
        }
    }
}

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
    public class InOrderController : Controller
    {
        private readonly InventoryContext _context;

        public InOrderController(InventoryContext context)
        {
            _context = context;
        }

        // GET: InOrder
        public async Task<IActionResult> Index(int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);

            }
            else
            {
                TempData["message"] = "You must login to view the Incoming Orders page";
                return Redirect("/Home");
            }
            var inventoryContext = _context.InOrder.Include(i => i.IdEmployeeNavigation).Include(i => i.IdShippingCompanyNavigation).Include(i => i.IdSupplierNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: InOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrder
                .Include(i => i.IdEmployeeNavigation)
                .Include(i => i.IdShippingCompanyNavigation)
                .Include(i => i.IdSupplierNavigation)
                .FirstOrDefaultAsync(m => m.IdinOrder == id);
            if (inOrder == null)
            {
                return NotFound();
            }

            return View(inOrder);
        }

        // GET: InOrder/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "Name");
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany");
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Idsupplier", "Namesupplier");
            return View();
        }

        // POST: InOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdinOrder,IdShippingCompany,IdSupplier,IdEmployee,Date")] InOrder inOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inOrder);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("inOrderID", inOrder.IdinOrder.ToString());
                return Redirect("/InItemOrder");
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "Name", inOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany", inOrder.IdShippingCompany);
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Idsupplier", "Namesupplier", inOrder.IdSupplier);
            return View(inOrder);
        }

        // GET: InOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrder.FindAsync(id);
            if (inOrder == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "Name", inOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Namecompany", inOrder.IdShippingCompany);
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Idsupplier", "Namesupplier", inOrder.IdSupplier);
            return View(inOrder);
        }

        // POST: InOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdinOrder,IdShippingCompany,IdSupplier,IdEmployee,Date")] InOrder inOrder)
        {
            if (id != inOrder.IdinOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InOrderExists(inOrder.IdinOrder))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "Idemployee", "Name", inOrder.IdEmployee);
            ViewData["IdShippingCompany"] = new SelectList(_context.ShippingCompany, "IdshippingCompany", "Nameocmpany", inOrder.IdShippingCompany);
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Idsupplier", "Namesupplier", inOrder.IdSupplier);
            return View(inOrder);
        }

        // GET: InOrder/Delete/5
        public async Task<IActionResult> Delete(int? id, int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);


                if (employee.Role == "USER")
                {
                    TempData["message"] = "You are not authorized to Delete orders";
                    return Redirect("/Home");
                }
            }
            
            if (id == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrder
                .Include(i => i.IdEmployeeNavigation)
                .Include(i => i.IdShippingCompanyNavigation)
                .Include(i => i.IdSupplierNavigation)
                .FirstOrDefaultAsync(m => m.IdinOrder == id);
            if (inOrder == null)
            {
                return NotFound();
            }

            return View(inOrder);
        }

        // POST: InOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inOrder = await _context.InOrder.FindAsync(id);
            _context.InOrder.Remove(inOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InOrderExists(int id)
        {
            return _context.InOrder.Any(e => e.IdinOrder == id);
        }
    }
}

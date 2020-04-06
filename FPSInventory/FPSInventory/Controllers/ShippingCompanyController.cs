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
    public class ShippingCompanyController : Controller
    {
        private readonly InventoryContext _context;

        public ShippingCompanyController(InventoryContext context)
        {
            _context = context;
        }

        // GET: ShippingCompany
        public async Task<IActionResult> Index()
        {
            var inventoryContext = _context.ShippingCompany.Include(s => s.IdCityNavigation);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: ShippingCompany/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingCompany = await _context.ShippingCompany
                .Include(s => s.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.IdshippingCompany == id);
            if (shippingCompany == null)
            {
                return NotFound();
            }

            return View(shippingCompany);
        }

        // GET: ShippingCompany/Create
        public IActionResult Create()
        {
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View();
        }

        // POST: ShippingCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdshippingCompany,Namecompany,Address,Phonenumber,IdCity")] ShippingCompany shippingCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View(shippingCompany);
        }

        // GET: ShippingCompany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingCompany = await _context.ShippingCompany.FindAsync(id);
            if (shippingCompany == null)
            {
                return NotFound();
            }
            ViewData["IdCity"] = _context.City
                            .Select(c => new SelectListItem
                            {
                                Text = c.Namecity + ", " + c.Province,
                                Value = c.Idcity.ToString()
                            });
            return View(shippingCompany);
        }

        // POST: ShippingCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdshippingCompany,Namecompany,Address,Phonenumber,IdCity")] ShippingCompany shippingCompany)
        {
            if (id != shippingCompany.IdshippingCompany)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingCompanyExists(shippingCompany.IdshippingCompany))
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
            return View(shippingCompany);
        }

        // GET: ShippingCompany/Delete/5
        public async Task<IActionResult> Delete(int? id, int Idemployee = 0)
        {
            if (HttpContext.Session.GetString("employeeID") != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString("employeeID"));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);


                if (employee.Role == "USER")
                {
                    TempData["message"] = "You are not authorized to Delete Shipping Companies";
                    return Redirect("/Home");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var shippingCompany = await _context.ShippingCompany
                .Include(s => s.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.IdshippingCompany == id);
            if (shippingCompany == null)
            {
                return NotFound();
            }

            return View(shippingCompany);
        }

        // POST: ShippingCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingCompany = await _context.ShippingCompany.FindAsync(id);
            _context.ShippingCompany.Remove(shippingCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingCompanyExists(int id)
        {
            return _context.ShippingCompany.Any(e => e.IdshippingCompany == id);
        }
    }
}

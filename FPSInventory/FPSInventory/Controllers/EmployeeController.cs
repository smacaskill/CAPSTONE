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
    public class EmployeeController : Controller
    {
        private readonly InventoryContext _context;

        public EmployeeController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index(int Idemployee = 0)
        {
            if (Idemployee != 0)
            {
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);

                if (employee == null)
                {
                    TempData["message"] = "That Employee is not on file";
                    return Redirect("/Home");
                }
                else
                {
                    HttpContext.Session.SetString("Idemployee", Idemployee.ToString());
                    HttpContext.Session.SetString("employeeName", employee.Name);
                    TempData["Message"] = "Thank you " + employee.Name + " for logging in";
                    return Redirect("/Home");
                }
            }
            else if (HttpContext.Session.GetString(nameof(Idemployee)) != null)
            {
                Idemployee = int.Parse(HttpContext.Session.GetString(nameof(Idemployee)));
                var employee = _context.Employee.FirstOrDefault(a => a.Idemployee == Idemployee);

                if (employee.Role == "USER")
                {
                    TempData["message"] = "You are not authorized to access the Employee page";
                    return Redirect("/Home");
                }
            }
            else
            {
                TempData["Message"] = "You must login first";
                return Redirect("/Home");
            }

            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Idemployee == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idemployee,Name,Email,Gender,Role")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("employeeName", employee.Name);

            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idemployee,Name,Email,Gender,Role")] Employee employee)
        {
            if (id != employee.Idemployee)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Idemployee))
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
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Idemployee == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Idemployee == id);
        }
    }
}

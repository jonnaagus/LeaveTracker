using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveTracker.Data;
using LeaveTracker.Models;

namespace LeaveTracker.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly LeaveTrackerDbContext _context;

        public LeaveApplicationsController(LeaveTrackerDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var leaveTrackerDbContext = _context.LeaveApplications.Include(l => l.Employee);
            return View(await leaveTrackerDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveTypeId == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            var employeesList = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.EmpFirstName} {e.EmpLastName}"
            }).ToList();

            ViewData["Employees"] = employeesList;

            return View();
        }


        // POST: LeaveApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveTypeId,LeaveType,StartDate,EndDate,FKEmployeeId")] LeaveApplication leaveApplication)
        {
            if (ModelState.IsValid)
            {
                // Sätt ApplicationDate till dagens datum och tid
                leaveApplication.ApplicationDate = DateTime.Now;

                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var employeesList = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.EmpFirstName} {e.EmpLastName}"
            }).ToList();

            ViewData["Employees"] = employeesList;
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["Employees"] = new SelectList(_context.Employees.Select(e => new { e.EmployeeId, FullName = $"{e.EmpFirstName} {e.EmpLastName}" }), "EmployeeId", "FullName", leaveApplication.FKEmployeeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveTypeId,LeaveType,StartDate,EndDate,ApplicationDate,FKEmployeeId")] LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.LeaveTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.LeaveTypeId))
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
            ViewData["FKEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveApplication.FKEmployeeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveTypeId == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.LeaveTypeId == id);
        }
    }
}

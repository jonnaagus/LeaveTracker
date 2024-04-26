using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using LeaveTracker.Data;
using LeaveTracker.Models;

namespace LeaveTracker.Controllers
{
    public class EmployeeLeaveHistoriesController : Controller
    {
        private readonly LeaveTrackerDbContext _context;

        public EmployeeLeaveHistoriesController(LeaveTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> LeaveHistory(int? leaveId)
        {
            var leaveApplications = await _context.LeaveApplications.ToListAsync();

            var query = from leave in _context.LeaveApplications
                        join emp in _context.Employees on leave.FKEmployeeId equals emp.EmployeeId
                        select new { emp, leave };

            if (leaveId.HasValue)
            {
                query = query.Where(x => x.leave.LeaveTypeId == leaveId.Value);
            }

            var employees = await query.Select(x => new EmployeeLeaveHistory
            {
                EmpFirstName = x.emp.EmpFirstName,
                EmpLastName = x.emp.EmpLastName,
                LeaveType = x.leave.LeaveType,
                ApplicationDate = x.leave.ApplicationDate
            }).ToListAsync();

            var viewModel = new LeaveApplicationViewModel
            {
                Employees = employees,
                LeaveApplications = leaveApplications,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchEmpLeave(int? employeeId)
        {
            var allEmployees = await _context.Employees.ToListAsync();
            ViewBag.AllEmployees = allEmployees;

            var leaveApplications = await _context.LeaveApplications.ToListAsync();

            var query = from leave in _context.LeaveApplications
                        join emp in _context.Employees on leave.FKEmployeeId equals emp.EmployeeId
                        select new { emp, leave };

            if (employeeId.HasValue)
            {
                query = query.Where(x => x.emp.EmployeeId == employeeId.Value);
            }

            var employees = await query.Select(x => new EmployeeLeaveHistory
            {
                EmpFirstName = x.emp.EmpFirstName,
                EmpLastName = x.emp.EmpLastName,
                LeaveType = x.leave.LeaveType,
                ApplicationDate = x.leave.ApplicationDate 
            }).ToListAsync();

            var viewModel = new LeaveApplicationViewModel
            {
                Employees = employees,
                LeaveApplications = leaveApplications,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LeaveApplicationMonth(int selectedMonth)
        {
            var leaveApplications = await _context.LeaveApplications
                .Where(x => x.ApplicationDate.Month == selectedMonth)
                .ToListAsync();

            var query = from leave in leaveApplications
                        join emp in _context.Employees on leave.FKEmployeeId equals emp.EmployeeId
                        select new { emp, leave };

            var employees = query.Select(x => new EmployeeLeaveHistory
            {
                EmpFirstName = x.emp.EmpFirstName,
                EmpLastName = x.emp.EmpLastName,
                LeaveType = x.leave.LeaveType
            }).ToList();

            var viewModel = new LeaveApplicationViewModel
            {
                Employees = employees,
                LeaveApplications = leaveApplications,
            };

            return View(viewModel);
        }
    }
}

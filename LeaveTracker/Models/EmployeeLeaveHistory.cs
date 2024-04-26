using System.ComponentModel.DataAnnotations;

namespace LeaveTracker.Models
{
    public class EmployeeLeaveHistory
    {
        [Key]
        public int LeaveHistoryId { get; set; }
        public int EmployeeId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string LeaveType { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
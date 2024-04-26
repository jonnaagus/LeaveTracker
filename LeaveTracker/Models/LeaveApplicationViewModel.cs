namespace LeaveTracker.Models
{
    public class LeaveApplicationViewModel
    {
        public IEnumerable<EmployeeLeaveHistory> Employees { get; set; }
        public IEnumerable<LeaveApplication> LeaveApplications { get; set; }

    }
}

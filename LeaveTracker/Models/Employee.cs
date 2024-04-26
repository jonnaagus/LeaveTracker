using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LeaveTracker.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [DisplayName("Förnamn")]
        public string EmpFirstName { get; set; }
        [DisplayName("Efternamn")]
        public string EmpLastName { get; set; }
        [DisplayName("Anställningsdatum")]
        public DateTime HireDate { get; set; }

        public virtual ICollection<LeaveApplication>? LeaveApplications { get; set; }
    }
}

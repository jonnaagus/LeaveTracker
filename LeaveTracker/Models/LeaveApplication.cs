using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveTracker.Models
{
    public class LeaveApplication
    {
        [Key]
        public int LeaveTypeId { get; set; }
        [DisplayName("Ledighetstyp")]
        public string LeaveType { get; set; }
        [DisplayName("Startdatum")]
        public DateTime StartDate { get; set; }
        [DisplayName("Slutdatum")]
        public DateTime EndDate { get; set; }
        [DisplayName("Ansökningsdatum")]
        public DateTime ApplicationDate { get; set; }

        [ForeignKey("Employee")]
        [DisplayName("AnställningsId")]
        public int FKEmployeeId { get; set; }
        [DisplayName("Anställd")]
        public Employee? Employee { get; set; }

    }
}

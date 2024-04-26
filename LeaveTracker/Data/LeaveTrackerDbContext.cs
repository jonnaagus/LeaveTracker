using LeaveTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracker.Data
{
    public class LeaveTrackerDbContext : DbContext
    {
        public LeaveTrackerDbContext(DbContextOptions<LeaveTrackerDbContext> options)
    : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<EmployeeLeaveHistory> EmployeeLeaveHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity
                <LeaveApplication>()
                .Property(a => a.ApplicationDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}



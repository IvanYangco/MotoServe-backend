using backend.Controllers;
using backend.Models;

public partial class MaintenanceHistory
{
    public int HistoryId { get; set; }

    public int CustomerId { get; set; }          // FK → Customer
    public int MechanicId { get; set; }          // FK → Mechanic
    public int MaintenanceId { get; set; }   // FK → MaintenanceType (Service)
   
    public int? ScheduleId { get; set; }          // FK → Schedule
    public int PaymentId { get; set; }           // FK → PaymentTable

    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public decimal Amount { get; set; }


    // public virtual Mechanic? Mechanic { get; set; }
    // public virtual Customer? Customer { get; set; }
    // public virtual MaintenanceType? MaintenanceType { get; set; }
    // public virtual MaintenanceSchedule? MaintenanceSchedule { get; set; }
    // public virtual PaymentTable? PaymentTable { get; set; }

    
}

using backend.Controllers;
using backend.Models;

public partial class MaintenanceHistory
{
    public int HistoryId { get; set; }

    public int? CustomerId { get; set; }     // must exist
    public int? MechanicId { get; set; }     // must exist
    public int? MaintenanceTypeId { get; set; }
    public int? CustomerMotorcycleId { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public decimal Amount { get; set; }

    public int? ScheduleId { get; set; } 
    public virtual Customer? Customer { get; set; }
    public virtual Mechanic? Mechanic { get; set; }
      public virtual MaintenanceSchedule? MaintenanceSchedule { get; set; }
        public virtual MaintenanceType? MaintenanceType { get; set; }
    public virtual CustomerMotorcycle? CustomerMotorcycle { get; set; }
}

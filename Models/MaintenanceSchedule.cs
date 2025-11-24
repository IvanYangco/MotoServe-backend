using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaintenanceSchedule
{
    
    // int HistoryId { get; set; }

    //public int? CustomerId { get; set; }
    public int? MechanicId { get; set; }
    //public int? MaintenanceTypeId { get; set; }
   // public int? CustomerMotorcycleId { get; set; }

    public int? ScheduleId { get; set; }  
    //public virtual MaintenanceSchedule? MaintenanceSchedule1 { get; set; }  // 🔥 ADD THIS
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    //public decimal Amount { get; set; }

    //public virtual Customer? Customer { get; set; }
    public virtual Mechanic? Mechanic { get; set; }
    //public virtual MaintenanceType? MaintenanceType { get; set; }
    //public virtual CustomerMotorcycle? CustomerMotorcycle { get; set; }
}


using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaintenanceHistory
{
    public int Id { get; set; }

    public int MotorcycleId { get; set; }

    public int? MechanicId { get; set; }

    public int? ScheduleId { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<MaintenanceHistoryType> MaintenanceHistoryTypes { get; set; } = new List<MaintenanceHistoryType>();

    public virtual Mechanic? Mechanic { get; set; }

    public virtual CustomerMotorcycle Motorcycle { get; set; } = null!;

    public virtual ICollection<PaymentTable> PaymentTables { get; set; } = new List<PaymentTable>();

    public virtual MaintenanceSchedule? Schedule { get; set; }
}

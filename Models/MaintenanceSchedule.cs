using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaintenanceSchedule
{
    public int ScheduleId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<MaintenanceHistory> MaintenanceHistories { get; set; } = new List<MaintenanceHistory>();
    public virtual ICollection<MaintenanceTypeAssignment> MaintenanceTypeAssignments { get; set; } = new List<MaintenanceTypeAssignment>();

}

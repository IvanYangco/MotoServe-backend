using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaintenanceType
{
    public int MaintenanceId { get; set; }

    public string MaintenanceName { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public string? Description { get; set; }

    public int? MechanicId { get; set; }

    public virtual Mechanic? Mechanic { get; set; }
    public virtual ICollection<MaintenanceHistoryType> MaintenanceHistoryTypes { get; set; } = new List<MaintenanceHistoryType>();

    public virtual ICollection<MaintenanceTypeAssignment> MaintenanceTypeAssignments { get; set; } = new List<MaintenanceTypeAssignment>();

}

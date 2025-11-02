using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaintenanceHistoryType
{
    public int Id { get; set; }

    public int MaintenanceHistoryId { get; set; }

    public int MaintenanceTypeId { get; set; }

    public virtual MaintenanceHistory MaintenanceHistory { get; set; } = null!;

    public virtual MaintenanceType MaintenanceType { get; set; } = null!;
}

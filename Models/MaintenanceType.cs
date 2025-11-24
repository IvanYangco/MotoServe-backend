using System.Collections.Generic;

namespace backend.Models
{
    public class MaintenanceType
    {
        public int MaintenanceId { get; set; }
        public string? MaintenanceName { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public int? MechanicId { get; set; }

        // 🔹 One Mechanic → Many MaintenanceTypes
        public Mechanic? Mechanic { get; set; }

        // 🔹 ADD THIS – REQUIRED for history tracking
        // public virtual ICollection<MaintenanceHistory> MaintenanceHistories { get; set; } = new List<MaintenanceHistory>();

        // // 🔹 Already existing many-to-many relationship
        // public virtual ICollection<MaintenanceTypeAssignment> MaintenanceTypeAssignments { get; set; } = new List<MaintenanceTypeAssignment>();
    }
}

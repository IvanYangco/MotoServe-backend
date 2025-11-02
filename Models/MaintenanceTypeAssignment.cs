namespace backend.Models
{
    public class MaintenanceTypeAssignment
    {
        public int Id { get; set; }
        public int MaintenanceTypeId { get; set; }
        public int ScheduleId { get; set; }

        public virtual MaintenanceType? MaintenanceType { get; set; }
        public virtual MaintenanceSchedule? MaintenanceSchedule { get; set; }
    }
}

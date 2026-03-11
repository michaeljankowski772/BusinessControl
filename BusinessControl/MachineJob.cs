namespace BusinessControlService
{
    public class MachineJob
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public required Worker Worker { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Description { get; set; } = string.Empty;
        public MaintenanceTypeEnum MaintenanceType { get; set; }
        public required Machine Machine { get; set; }

    }
}

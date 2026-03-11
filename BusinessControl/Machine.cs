namespace BusinessControlService
{
    public class Machine
    {
        public int Id { get; set; }
        public MachineTypeEnum MachineType { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string MachineSimpleName { get; set; } = string.Empty;
        public DateTime? AcquisitionDate { get; set; }
        public DateTime? DecomissionDate { get; set; }


    }
}

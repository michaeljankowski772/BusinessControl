namespace BusinessControlService
{
    public class FieldJob
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public required Worker Worker { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public MachineTypeEnum MachineType { get; set; }


    }
}

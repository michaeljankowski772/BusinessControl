namespace BusinessControlService.Models
{
    public class FieldJobDTO
    {
        public int Id { get; set; }
        public required int CustomerId { get; set; }
        public required string CustomerFirstName { get; set; }
        public required string CustomerLastName { get; set; }
        public required int WorkerId { get; set; }
        public required string WorkerFirstName { get; set; }
        public required string WorkerLastName { get; set; }
        public required int MachineId { get; set; }
        public required string MachineName { get; set; }
        public required float FieldArea { get; set; }
    }
}

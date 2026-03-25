namespace BusinessControlService.Models
{
    public class FieldJob
    {
        public int Id { get; set; }
        public required int WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public required int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public required int MachineId { get; set; }
        public Machine? Machine { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public float FieldArea { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float FieldAreaAccepted { get; set; }
        public float PricePerArea { get; set; }
        public float PricePerAreaAccepted { get; set; }




    }
}

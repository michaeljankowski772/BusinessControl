namespace BusinessControlService.Models
{
    public class FieldJob
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public required Worker Worker { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public required Machine Machine { get; set; }
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

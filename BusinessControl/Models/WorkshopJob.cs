namespace BusinessControlService.Models
{
    public class WorkshopJob
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public required Worker Worker { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Description { get; set; } = string.Empty;



    }
}

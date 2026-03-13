namespace BusinessControlService.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsHired { get; set; }
        public DateTime? HiringDateStart { get; set; }
        public DateTime? HiringDateEnd { get; set; }


    }
}

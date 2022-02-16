namespace CD.Models
{
    public class Lending
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }
        
        public int CdId { get; set; }
        public Cd? Cd { get; set; }
    }
}


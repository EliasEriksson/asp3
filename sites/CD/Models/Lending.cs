using System.ComponentModel.DataAnnotations;

namespace CD.Models
{
    public class Lending
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Borrowed")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        
        [Required]
        public int CdId { get; set; }
        public Cd? Cd { get; set; }

        public Lending(int cdId, int userId)
        {
            this.CdId = cdId;
            this.UserId = userId;
        }
    }
}


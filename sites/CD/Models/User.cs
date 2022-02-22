using System.ComponentModel.DataAnnotations;

namespace CD.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        public List<Lending>? Lendings { get; set; }
    }
}
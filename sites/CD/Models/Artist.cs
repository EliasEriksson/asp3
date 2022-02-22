using System.ComponentModel.DataAnnotations;

namespace CD.Models
{
    public class Artist
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        public List<Cd>? Cds { get; set; }
    }
}
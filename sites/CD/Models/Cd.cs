using System.ComponentModel.DataAnnotations;

namespace CD.Models
{
    public class Cd
    {
        public int Id { get; set; }
        
        public string? Title { get; set; }
        
        public string? Category { get; set; }
        
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
        
        public Lending? Lending { get; set; }
    }
}
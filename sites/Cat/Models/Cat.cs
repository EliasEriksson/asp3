using Microsoft.Build.Framework;

namespace Cat.Models
{
    public class Cat
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }
}


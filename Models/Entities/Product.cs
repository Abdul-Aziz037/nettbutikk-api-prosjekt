using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nettbutikk_api.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price  { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; } // Referanse til brukerens ID

        // Navigasjonsegenskap for User (valgfritt)
        public User? User { get; set; }
    } 
}

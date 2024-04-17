using System.ComponentModel.DataAnnotations;

namespace nettbutikk_api.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "produktnavn er påkrevd.")]
        [StringLength(50, ErrorMessage = "Type arrangement kan ikke være lengre enn 50 tegn.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Beskrivelse er påkrevd.")]
        [StringLength(255, ErrorMessage = "Beskrivelsen kan ikke være lengre enn 255 tegn.")]
        public string? Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Pris er påkrevd.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Prisen må være større enn null.")]
        public decimal Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace nettbutikk_api.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Brukernavn er påkrevd.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Passord er påkrevd.")]
        public string Password { get; set; }
    }
}

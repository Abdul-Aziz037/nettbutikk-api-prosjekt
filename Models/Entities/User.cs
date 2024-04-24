using System.ComponentModel.DataAnnotations;

namespace nettbutikk_api.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Lagre passordet som en hash-verdi

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        // Navigasjonsegenskap for eierrelasjonen
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

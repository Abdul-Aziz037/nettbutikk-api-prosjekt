namespace nettbutikk_api.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Lagre passordet som en hash-verdi
        public string Email { get; set; }
        // Legg til flere egenskaper om nødvendig

        // Navigasjonsegenskap for eierrelasjonen
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

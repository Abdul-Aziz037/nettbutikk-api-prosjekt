namespace nettbutikk_api.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }

    }
}

namespace nettbutikk_api.Models.DTOs
{
    public record UserDTO(
    int Id,
    string? UserName,
    string? FirstName,
    string? LastName,
    string? Email,
    DateTime created,
    DateTime updated);
}

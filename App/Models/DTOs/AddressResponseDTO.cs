namespace App.Models.DTOs;

public class AddressResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string PublicPlace { get; set; } = string.Empty;
    public string? Complement { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string FederalUnit { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int UserId { get; set; }
}

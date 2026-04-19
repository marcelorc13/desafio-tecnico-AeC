using System.ComponentModel.DataAnnotations;

namespace App.Models.DTOs;

public class AddressCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(8)]
    public string CEP { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string PublicPlace { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Complement { get; set; }

    [Required]
    [MaxLength(100)]
    public string District { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(2)]
    public string FederalUnit { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Number { get; set; } = string.Empty;
}

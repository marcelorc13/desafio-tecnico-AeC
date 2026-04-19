using System.ComponentModel.DataAnnotations;

namespace App.Models.DTOs;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

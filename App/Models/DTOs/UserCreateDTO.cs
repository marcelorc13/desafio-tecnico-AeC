using System.ComponentModel.DataAnnotations;

namespace App.Models.DTOs;

public class UserCreateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nome de usuário é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome de usuário deve ter no mínimo 3 caracteres")]
    [MaxLength(50, ErrorMessage = "Nome de usuário deve ter no máximo 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Nome de usuário deve conter apenas letras, números e underline")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
    [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Senha deve conter ao menos uma letra maiúscula, uma minúscula, um número e um caractere especial")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
    [Compare("Password", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

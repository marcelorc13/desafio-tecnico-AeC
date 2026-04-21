using System.ComponentModel.DataAnnotations;

public class ProfileUpdateDTO
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nome de usuário é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome de usuário deve ter no mínimo 3 caracteres")]
    [MaxLength(50, ErrorMessage = "Nome de usuário deve ter no máximo 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Nome de usuário deve conter apenas letras, números e underline")]
    public string Username { get; set; } = string.Empty;
}

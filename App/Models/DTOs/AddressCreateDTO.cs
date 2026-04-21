using System.ComponentModel.DataAnnotations;
namespace App.Models.DTOs;

public class AddressCreateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "CEP é obrigatório")]
    [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve conter exatamente 8 dígitos")]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "Logradouro é obrigatório")]
    [MaxLength(200, ErrorMessage = "Logradouro deve ter no máximo 200 caracteres")]
    public string PublicPlace { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres")]
    public string? Complement { get; set; }

    [Required(ErrorMessage = "Bairro é obrigatório")]
    [MaxLength(100, ErrorMessage = "Bairro deve ter no máximo 100 caracteres")]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cidade é obrigatória")]
    [MaxLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "UF é obrigatória")]
    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "UF deve conter 2 letras maiúsculas (ex: AL, MG)")]
    [EnumDataType(typeof(Enums.FederalUnit), ErrorMessage = "UF inválida")]
    public string FederalUnit { get; set; } = string.Empty;

    [Required(ErrorMessage = "Número é obrigatório")]
    [RegularExpression(@"^(\d+[A-Za-z]?|[Ss]/[Nn])$", ErrorMessage = "Número deve ser numérico (ex: 123 ou 123A) ou S/N")]
    public string Number { get; set; } = string.Empty;
}

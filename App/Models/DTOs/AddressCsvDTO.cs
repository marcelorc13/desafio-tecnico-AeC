namespace App.Models.DTOs;

public class AddressCsvDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string UF { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}

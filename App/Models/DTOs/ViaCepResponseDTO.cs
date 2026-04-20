using System.Text.Json.Serialization;

namespace App.Models.DTOs;

public class ViaCepResponseDto
{
    [JsonPropertyName("cep")]
    public string CEP { get; set; } = string.Empty;

    [JsonPropertyName("logradouro")]
    public string PublicPlace { get; set; } = string.Empty;

    [JsonPropertyName("complemento")]
    public string Complement { get; set; } = string.Empty;

    [JsonPropertyName("bairro")]
    public string District { get; set; } = string.Empty;

    [JsonPropertyName("localidade")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("uf")]
    public string FederalUnit { get; set; } = string.Empty;

    [JsonPropertyName("erro")]
    public bool Erro { get; set; }
}

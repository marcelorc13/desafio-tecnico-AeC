using System.Text.Json;
using App.Integrations.Interfaces;
using App.Models.DTOs;

namespace App.Integrations;

public class ViaCepIntegration : IViaCepIntegration
{
    private readonly HttpClient _http;
    private readonly ILogger<ViaCepIntegration> _logger;

    public ViaCepIntegration(IHttpClientFactory factory, ILogger<ViaCepIntegration> logger)
    {
        _http = factory.CreateClient("viacep");
        _logger = logger;
    }

    public async Task<ViaCepResponseDto?> GetAddressByCep(string cep)
    {
        cep = new string(cep.Where(char.IsDigit).ToArray());

        if (cep.Length != 8)
        {
            _logger.LogWarning("CEP inválido: {CEP}", cep);
            return null;
        }

        try
        {
            _logger.LogInformation("Consultando ViaCEP para o CEP: {CEP}", cep);

            var response = await _http.GetAsync($"ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("ViaCEP retornou status {Status} para o CEP {CEP}",
                    response.StatusCode, cep);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ViaCepResponseDto>(json);

            if (result == null || result.Erro)
            {
                _logger.LogWarning("CEP não encontrado: {CEP}", cep);
                return null;
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao conectar com ViaCEP para o CEP {CEP}", cep);
            return null;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Timeout ao consultar ViaCEP para o CEP {CEP}", cep);
            return null;
        }
    }
}

using App.Models.DTOs;

namespace App.Integrations.Interfaces;

public interface IViaCepIntegration
{
    Task<ViaCepResponseDto?> GetAddressByCep(string cep);
}

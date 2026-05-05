using OficinaMecanica.DTOs;

namespace OficinaMecanica.Services.Interfaces
{
    public interface IOrcamentoService
    {
        OrcamentoResponse CriarOrcamento(CriarOrcamentoRequest request);
    }
}

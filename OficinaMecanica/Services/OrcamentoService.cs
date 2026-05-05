using OficinaMecanica.DTOs;
using OficinaMecanica.Models;
using OficinaMecanica.Repositories.Interfaces;
using OficinaMecanica.Services.Interfaces;

namespace OficinaMecanica.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository)
        {
            _orcamentoRepository = orcamentoRepository;
        }

        public OrcamentoResponse CriarOrcamento(CriarOrcamentoRequest request)
        {
            var itens = request.Itens.Select(i => new ItemOrcamento
            {
                Descricao = i.Descricao,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario
            }).ToList();

            var orcamento = new Orcamento
            {
                ClienteId = request.ClienteId!.Value, 
                VeiculoId = request.VeiculoId!.Value,  
                Itens = itens,
                Total = itens.Sum(i => i.Subtotal),
                CriadoEm = DateTime.UtcNow
            };

            var salvo = _orcamentoRepository.Salvar(orcamento);

            return MapearParaResponse(salvo);
        }

    
        private static OrcamentoResponse MapearParaResponse(Orcamento orcamento) =>
            new()
            {
                Id = orcamento.Id,
                ClienteId = orcamento.ClienteId,
                VeiculoId = orcamento.VeiculoId,
                Total = orcamento.Total,
                CriadoEm = orcamento.CriadoEm,
                Itens = orcamento.Itens.Select(i => new ItemOrcamentoResponse
                {
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
    }
}

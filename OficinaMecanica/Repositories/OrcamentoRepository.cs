using OficinaMecanica.Models;
using OficinaMecanica.Repositories.Interfaces;

namespace OficinaMecanica.Repositories
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly List<Orcamento> _orcamentos = [];
        private int _proximoId = 1;

        public Orcamento Salvar(Orcamento orcamento)
        {
            orcamento.Id = _proximoId++;
            _orcamentos.Add(orcamento);
            return orcamento;
        }

        public Orcamento? BuscarPorId(int id)
            => _orcamentos.FirstOrDefault(o => o.Id == id);

        public IEnumerable<Orcamento> BuscarTodos()
            => _orcamentos.AsReadOnly();
    }
}

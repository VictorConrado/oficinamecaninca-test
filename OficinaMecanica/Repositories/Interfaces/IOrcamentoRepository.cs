using OficinaMecanica.Models;

namespace OficinaMecanica.Repositories.Interfaces
{
    public interface IOrcamentoRepository
    {
        Orcamento Salvar(Orcamento orcamento);
        Orcamento? BuscarPorId(int id);
        IEnumerable<Orcamento> BuscarTodos();
    }
}

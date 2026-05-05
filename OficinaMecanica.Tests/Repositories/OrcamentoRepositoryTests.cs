using OficinaMecanica.Models;
using OficinaMecanica.Repositories;
using Xunit;

namespace OficinaMecanica.Tests.Repositories;

public class OrcamentoRepositoryTests
{
    private readonly OrcamentoRepository _repository;

    public OrcamentoRepositoryTests()
    {
        _repository = new OrcamentoRepository();
    }

    [Fact]
    public void Salvar_DevePersistirOrcamento_ERetornarComId()
    {
        // Arrange
        var orcamento = CriarOrcamentoValido();

        // Act
        var resultado = _repository.Salvar(orcamento);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.Id > 0);
    }

    [Fact]
    public void Salvar_DevePersistirOrcamento_EPoderBuscarPorId()
    {
        // Arrange
        var orcamento = CriarOrcamentoValido();

        // Act
        var salvo = _repository.Salvar(orcamento);
        var encontrado = _repository.BuscarPorId(salvo.Id);

        // Assert
        Assert.NotNull(encontrado);
        Assert.Equal(salvo.Id, encontrado.Id);
        Assert.Equal(salvo.ClienteId, encontrado.ClienteId);
    }

    [Fact]
    public void BuscarPorId_DeveRetornarNull_QuandoNaoExistir()
    {
        // Arrange
        const int idInexistente = 9999;

        // Act
        var resultado = _repository.BuscarPorId(idInexistente);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void BuscarTodos_DeveRetornarTodosOrcamentosSalvos()
    {
        // Arrange
        _repository.Salvar(CriarOrcamentoValido());
        _repository.Salvar(CriarOrcamentoValido());

        // Act
        var todos = _repository.BuscarTodos().ToList();

        // Assert
        Assert.Equal(2, todos.Count);
    }

    private static Orcamento CriarOrcamentoValido() => new()
    {
        ClienteId = 10,
        VeiculoId = 25,
        CriadoEm = DateTime.UtcNow,
        Itens =
        [
            new ItemOrcamento { Descricao = "Troca de óleo", Quantidade = 1, ValorUnitario = 120m },
            new ItemOrcamento { Descricao = "Filtro de óleo", Quantidade = 1, ValorUnitario = 45m }
        ],
        Total = 165m
    };
}
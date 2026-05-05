using Moq;
using OficinaMecanica.DTOs;
using OficinaMecanica.Models;
using OficinaMecanica.Repositories.Interfaces;
using OficinaMecanica.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OficinaMecanica.Tests.Services
{
    public class OrcamentoServiceTests
    {
        private readonly Mock<IOrcamentoRepository> _repositoryMock;
        private readonly OrcamentoService _service;

        public OrcamentoServiceTests()
        {
            _repositoryMock = new Mock<IOrcamentoRepository>();
            _service = new OrcamentoService(_repositoryMock.Object);
        }

        [Fact]
        public void CriarOrcamento_DeveCalcularTotalCorretamente()
        {
            // Arrange
            var request = CriarRequestValido();

            _repositoryMock
                .Setup(r => r.Salvar(It.IsAny<Orcamento>()))
                .Returns((Orcamento o) => { o.Id = 1; return o; });

            // Act
            var resultado = _service.CriarOrcamento(request);

            // Assert
            Assert.Equal(165m, resultado.Total);
        }

        [Fact]
        public void CriarOrcamento_DeveCalcularSubtotalPorItem()
        {
            // Arrange
            var request = new CriarOrcamentoRequest
            {
                ClienteId = 1,
                VeiculoId = 1,
                Itens =
                [
                    new ItemOrcamentoRequest { Descricao = "Pastilha de freio", Quantidade = 4, ValorUnitario = 50m }
                ]
            };

            _repositoryMock
                .Setup(r => r.Salvar(It.IsAny<Orcamento>()))
                .Returns((Orcamento o) => { o.Id = 1; return o; });

            // Act
            var resultado = _service.CriarOrcamento(request);

            // Assert
            Assert.Equal(200m, resultado.Total);
            Assert.Equal(200m, resultado.Itens[0].Subtotal);
        }

        [Fact]
        public void CriarOrcamento_DeveMapearClienteEVeiculoCorretamente()
        {
            // Arrange
            var request = CriarRequestValido();

            _repositoryMock
                .Setup(r => r.Salvar(It.IsAny<Orcamento>()))
                .Returns((Orcamento o) => { o.Id = 1; return o; });

            // Act
            var resultado = _service.CriarOrcamento(request);

            // Assert
            Assert.Equal(10, resultado.ClienteId);
            Assert.Equal(25, resultado.VeiculoId);
        }

        [Fact]
        public void CriarOrcamento_DeveChamarRepositorioUmaVez()
        {
            // Arrange
            var request = CriarRequestValido();

            _repositoryMock
                .Setup(r => r.Salvar(It.IsAny<Orcamento>()))
                .Returns((Orcamento o) => { o.Id = 1; return o; });

            // Act
            _service.CriarOrcamento(request);

            // Assert
            _repositoryMock.Verify(r => r.Salvar(It.IsAny<Orcamento>()), Times.Once);
        }

        [Fact]
        public void CriarOrcamento_DeveRetornarIdAtribuidoPeloRepositorio()
        {
            // Arrange
            var request = CriarRequestValido();

            _repositoryMock
                .Setup(r => r.Salvar(It.IsAny<Orcamento>()))
                .Returns((Orcamento o) => { o.Id = 42; return o; });

            // Act
            var resultado = _service.CriarOrcamento(request);

            // Assert
            Assert.Equal(42, resultado.Id);
        }

        private static CriarOrcamentoRequest CriarRequestValido() => new()
        {
            ClienteId = 10,
            VeiculoId = 25,
            Itens =
            [
                new ItemOrcamentoRequest { Descricao = "Troca de óleo", Quantidade = 1, ValorUnitario = 120m },
            new ItemOrcamentoRequest { Descricao = "Filtro de óleo", Quantidade = 1, ValorUnitario = 45m }
            ]
        };
    }
}

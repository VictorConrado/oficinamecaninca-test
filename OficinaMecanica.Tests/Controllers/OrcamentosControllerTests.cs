using Microsoft.AspNetCore.Mvc;
using Moq;
using OficinaMecanica.Controllers;
using OficinaMecanica.DTOs;
using OficinaMecanica.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OficinaMecanica.Tests.Controllers
{
    public class OrcamentosControllerTests
    {
        private readonly Mock<IOrcamentoService> _serviceMock;
        private readonly OrcamentoController _controller;

        public OrcamentosControllerTests()
        {
            _serviceMock = new Mock<IOrcamentoService>();
            _controller = new OrcamentoController(_serviceMock.Object);
        }

        [Fact]
        public void CriarOrcamento_DeveRetornar201_QuandoRequestValido()
        {
            // Arrange
            var request = CriarRequestValido();

            _serviceMock
                .Setup(s => s.CriarOrcamento(request))
                .Returns(CriarResponseFake());

            // Act
            var resultado = _controller.CriarOrcamento(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void CriarOrcamento_DeveRetornarOrcamentoNaResposta()
        {
            // Arrange
            var request = CriarRequestValido();

            _serviceMock
                .Setup(s => s.CriarOrcamento(request))
                .Returns(CriarResponseFake());

            // Act
            var resultado = _controller.CriarOrcamento(request) as CreatedAtActionResult;

            // Assert
            var body = Assert.IsType<OrcamentoResponse>(resultado!.Value);
            Assert.Equal(1, body.Id);
            Assert.Equal(165m, body.Total);
        }

        [Fact]
        public void CriarOrcamento_DeveChamarServiceUmaVez()
        {
            // Arrange
            var request = CriarRequestValido();

            _serviceMock
                .Setup(s => s.CriarOrcamento(request))
                .Returns(CriarResponseFake());

            // Act
            _controller.CriarOrcamento(request);

            // Assert
            _serviceMock.Verify(s => s.CriarOrcamento(request), Times.Once);
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

        private static OrcamentoResponse CriarResponseFake() => new()
        {
            Id = 1,
            ClienteId = 10,
            VeiculoId = 25,
            Total = 165m,
            CriadoEm = DateTime.UtcNow,
            Itens =
            [
                new ItemOrcamentoResponse { Descricao = "Troca de óleo", Quantidade = 1, ValorUnitario = 120m, Subtotal = 120m },
            new ItemOrcamentoResponse { Descricao = "Filtro de óleo", Quantidade = 1, ValorUnitario = 45m, Subtotal = 45m }
            ]
        };
    }
}

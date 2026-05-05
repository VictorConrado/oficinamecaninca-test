using Microsoft.AspNetCore.Mvc;
using OficinaMecanica.DTOs;
using OficinaMecanica.Services.Interfaces;

namespace OficinaMecanica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentoController : ControllerBase
    {

        private readonly IOrcamentoService _orcamentoService;

        public OrcamentoController(IOrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrcamentoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarOrcamento([FromBody] CriarOrcamentoRequest request)
        {
            var response = _orcamentoService.CriarOrcamento(request);
            return CreatedAtAction(nameof(CriarOrcamento), new { id = response.Id }, response);
        }
    }
}

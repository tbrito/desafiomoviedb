using Desafio.Core.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IConsultaFilmeServico consultaFilmeServico;

        public FilmesController(IConsultaFilmeServico consultaFilmeServico)
        {
            this.consultaFilmeServico = consultaFilmeServico;
        }

        [HttpGet, Route("Estreias")]
        public IActionResult Estreias()
        {
            var estreias = this.consultaFilmeServico.ObterTodasEstreias();
            return Ok(estreias);
        }
    }
}
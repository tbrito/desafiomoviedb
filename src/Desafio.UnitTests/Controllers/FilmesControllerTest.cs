using Desafio.Api.Controllers;
using Desafio.Core.Contratos;
using Desafio.Core.Output;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Desafio.UnitTests
{
    public class FilmesControllerTest
    {
        /// <summary>
        /// Testar o comportamento b�sico da controller
        /// </summary>
        [Fact]
        public void DeveRetornarFilmesEstreando()
        {
            //// Cenario
            var consultaFilmeServico = new Mock<IConsultaFilmeServico>();
            
            var estreias = new Estreias()
            {
                Filmes = new System.Collections.Generic.List<Filme>
                {
                    new Filme { Titulo = "Era do gelo" }
                }
            };

            consultaFilmeServico
                .Setup(s => s.ObterTodasEstreias())
                .Returns(estreias);

            //// Execu��o
            var controller = new FilmesController(consultaFilmeServico.Object);
            var result = controller.Estreias();

            //// Verifica��o
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<Estreias>(viewResult.Value);
            model.Filmes.Count.Should().Be(1);
        }
    }
}

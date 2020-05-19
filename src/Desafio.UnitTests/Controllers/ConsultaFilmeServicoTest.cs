using Desafio.Core.Contratos;
using Desafio.Core.Output;
using Desafio.Core.Servicos;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Desafio.UnitTests.Controllers
{
    public class ConsultaFilmeServicoTest
    {
        [Fact]
        public void DeveRetornarFilmesComGenero()
        {
            var movieApi = new Mock<IMovieApi>();
            var generoApi = new Mock<IGeneroApi>();

            var estreias = new Estreias()
            {
                Filmes = new List<Filme>
                {
                    new Filme { Titulo = "era do gelo", GeneroIds = new[] { 12, 16 } },
                    new Filme { Titulo = "detona ralph", GeneroIds = new[] { 16, 35 } }
                }
            };

            var generos = new Generos
            {
                Genero = new List<Genero>
                {
                    new Genero { Id = 12, Nome = "Aventura" },
                    new Genero { Id = 16, Nome = "Animação" },
                    new Genero { Id = 35, Nome = "Comédia" },
                    new Genero { Id = 10402, Nome = "Música" }
                }
            };

            movieApi
                .Setup(s => s.ObterTodasEstreias())
                .Returns(estreias);

            generoApi
                .Setup(s => s.ObterTodos())
                .Returns(generos);

            var consultaFilme = new ConsultaFilmeServico(movieApi.Object, generoApi.Object);
            var result = consultaFilme.ObterTodasEstreias();

            var eraDoGelo = result.Filmes.ElementAt(0);

            eraDoGelo.Titulo.Should().Be("era do gelo");
            eraDoGelo.Generos[0].Nome.Should().Be("Aventura");
            eraDoGelo.Generos[1].Nome.Should().Be("Animação");

            var detonaRalph = result.Filmes.ElementAt(1);

            detonaRalph.Titulo.Should().Be("detona ralph");
            detonaRalph.Generos[0].Nome.Should().Be("Animação");
            detonaRalph.Generos[1].Nome.Should().Be("Comédia");
        }
    }
}

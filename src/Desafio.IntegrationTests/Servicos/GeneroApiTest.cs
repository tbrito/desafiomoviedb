using Desafio.Core.Settings;
using Desafio.Infra.Servicos;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Desafio.IntegrationTests.Servicos
{
    public class GeneroApiTest
    {
        private readonly IOptions<EndPoints> endpoints;

        public GeneroApiTest()
        {
            this.endpoints = Options.Create<EndPoints>(new EndPoints
            {
                TheMovieDbSettings = new TheMovieDbSettings
                {
                    Uri = "https://api.themoviedb.org/3",
                    Key = "c3ef6199d9ec6c9b7e2a2b34edcc379c"
                }
            });
        }

        [Fact]
        public void DeveRetornarGeneros()
        {
            var generoApi = new GeneroApi(this.endpoints);

            var resultado = generoApi.ObterTodos();

            resultado.Genero.Count.Should().BeGreaterThan(1);
        }
    }
}

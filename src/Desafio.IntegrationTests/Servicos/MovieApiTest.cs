using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Desafio.Infra.Servicos;
using System;
using Desafio.Core.Settings;

namespace Desafio.IntegrationTests.Servicos
{
    /// <summary>
    /// Testamos a integração com a api do themoviedb
    /// </summary>
    public class MovieApiTest
    {
        private readonly IOptions<EndPoints> endpoints;

        public MovieApiTest()
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
        public void DeveRetornarEstreias()
        {
            var theMovieDb = new MovieApi(this.endpoints);

            var resultado = theMovieDb.ObterEstreias();

            resultado.Filmes.Count.Should().Be(20);
        }

        [Fact]
        public void DeveRetornarEstreiasNaoLimitadoA20Resultados()
        {
            var theMovieDb = new MovieApi(this.endpoints);

            var resultado = theMovieDb.ObterTodasEstreias();

            resultado.Filmes.Count.Should().BeGreaterThan(20);
        }
    }
}

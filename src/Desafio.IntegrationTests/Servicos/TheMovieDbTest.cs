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
    public class TheMovieDbTest
    {
        private readonly IOptions<EndPoints> endpoints;

        public TheMovieDbTest()
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
            var theMovieDb = new TheMovieDb(this.endpoints);

            var resultado = theMovieDb.ObterEstreias();

            resultado.Filmes.Count.Should().Be(20);
        }

        [Fact]
        public void DeveRetornarEstreiasNaoLimitadoA20Resultados()
        {
            var theMovieDb = new TheMovieDb(this.endpoints);

            var resultado = theMovieDb.ObterTodasEstreias();

            resultado.Filmes.Count.Should().BeGreaterThan(20);
        }

        [Fact]
        public void DeveDispararExcecaoCasoNaoExistaParametrosDaApi()
        {
            var endpointVazio = Options.Create<EndPoints>(new EndPoints
            {
                TheMovieDbSettings = null
            });

            var theMovieDb = new TheMovieDb(endpointVazio);

            var resultado = theMovieDb
                .Invoking(y => y.ObterTodasEstreias())
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(
@"Não foi possível estabelecer conexão.
Parametros de conexão com TheMovieDb.org não foram encontrados.
Verifique appsettings.json");
        }
    }
}

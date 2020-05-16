using Desafio.Core.Contratos;
using Desafio.Core.Output;
using Desafio.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Desafio.Infra.Servicos
{
    public class TheMovieDb : ServicoRest, ITheMovieDb
    {
        public TheMovieDb(IOptions<EndPoints> options) : 
            base(options)
        {
        }

        public Estreias ObterTodasEstreias()
        {
            if (this.endpoints.TheMovieDbSettings == null)
            {
                throw new InvalidOperationException(
@"Não foi possível estabelecer conexão.
Parametros de conexão com TheMovieDb.org não foram encontrados.
Verifique appsettings.json");
            }

            var estreias = this.ObterEstreias();

            if (estreias.TotalPaginas == 1)
            {
                return estreias;
            }

            for (int pagina = 2; pagina <= estreias.TotalPaginas; pagina++)
            {
                var estreia = this.ObterEstreias(pagina);
                estreias.Filmes.AddRange(estreia.Filmes);
            }
            
            return estreias;
        }

        public Estreias ObterEstreias(int pagina = 1)
        {
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = "movie/upcoming",
            };

            request.AddParameter("api_key", this.endpoints.TheMovieDbSettings.Key, ParameterType.QueryString);
            request.AddParameter("page", pagina, ParameterType.QueryString);

            var response = this.Executar(request);

            var estreias = JsonConvert.DeserializeObject<Estreias>(response.Content);

            return estreias;
        }
    }
}

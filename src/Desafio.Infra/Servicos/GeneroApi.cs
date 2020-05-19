using Desafio.Core.Contratos;
using Desafio.Core.Output;
using Desafio.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Desafio.Infra.Servicos
{
    public class GeneroApi : ServicoRest, IGeneroApi
    {
        public GeneroApi(IOptions<EndPoints> options) : 
            base(options)
        {
        }

        public Generos ObterTodos()
        {
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = "genre/movie/list",
            };

            request.AddParameter("api_key", this.endpoints.TheMovieDbSettings.Key, ParameterType.QueryString);
            request.AddParameter("language", this.endpoints.TheMovieDbSettings.Idioma, ParameterType.QueryString);

            var response = this.Executar(request);

            var generos = JsonConvert.DeserializeObject<Generos>(response.Content);

            return generos;
        }
    }
}

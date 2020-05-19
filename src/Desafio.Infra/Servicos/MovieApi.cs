using Desafio.Core.Contratos;
using Desafio.Core.Output;
using Desafio.Core.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Desafio.Infra.Servicos
{
    public class MovieApi : ServicoRest, IMovieApi
    {
        public MovieApi(IOptions<EndPoints> options) : 
            base(options)
        {
        }

        public Estreias ObterTodasEstreias()
        {
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
            request.AddParameter("language", this.endpoints.TheMovieDbSettings.Idioma, ParameterType.QueryString);
            request.AddParameter("page", pagina, ParameterType.QueryString);

            var response = this.Executar(request);

            var estreias = JsonConvert.DeserializeObject<Estreias>(response.Content);

            return estreias;
        }
    }
}

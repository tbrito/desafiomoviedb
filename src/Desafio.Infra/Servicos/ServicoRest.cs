using Desafio.Core.Settings;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Net;

namespace Desafio.Infra.Servicos
{
    public abstract class ServicoRest
    {
        protected readonly EndPoints endpoints;
        private RestClient client;
        
        public ServicoRest(IOptions<EndPoints> options)
        {
            this.endpoints = options.Value;
        }

        protected IRestResponse Executar(RestRequest request)
        {
            var response = this.GetClient().Execute(request);

            this.DispararExcecaoSeRetornoNaoForOk(request, response);

            return response;
        }

        private RestClient GetClient()
        {
            if (this.client == null)
            {
                this.client = new RestClient(this.endpoints.TheMovieDbSettings.Uri);
            }

            return this.client;
        }

        private void DispararExcecaoSeRetornoNaoForOk(RestRequest request, IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    string.Format(
                        "Erro ao comunicar com {0}: {1} Conteúdo: {2}", 
                        request.Resource, 
                        response.StatusDescription, 
                        response.Content));
            }
        }
    }
}
using Desafio.Core.Contratos;
using Desafio.Core.Output;
using System.IO;
using System.Linq;

namespace Desafio.Core.Servicos
{
    public class ConsultaFilmeServico : IConsultaFilmeServico
    {
        private readonly IMovieApi movieApi;
        private readonly IGeneroApi generoApi;

        public ConsultaFilmeServico(
            IMovieApi movieApi, 
            IGeneroApi generoApi)
        {
            this.movieApi = movieApi;
            this.generoApi = generoApi;
        }

        public Estreias ObterTodasEstreias()
        {
            var estreias = this.movieApi.ObterTodasEstreias();
            var generos = this.generoApi.ObterTodos();

            foreach (var filme in estreias.Filmes)
            {
                filme.Generos.AddRange(from generoId in filme.GeneroIds
                                       let genero = generos.Genero.First(x => x.Id == generoId)
                                       select genero);
            }

            return estreias;
        }
    }
}

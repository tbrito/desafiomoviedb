using Desafio.Core.Contratos;
using Desafio.Core.Output;

namespace Desafio.Core.Servicos
{
    public class ConsultaFilmeServico : IConsultaFilmeServico
    {
        private readonly ITheMovieDb movieDb;

        public ConsultaFilmeServico(ITheMovieDb movieDb)
        {
            this.movieDb = movieDb;
        }

        public Estreias ObterTodasEstreias()
        {
            return this.movieDb.ObterTodasEstreias();
        }
    }
}

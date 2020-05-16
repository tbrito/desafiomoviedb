using Desafio.Core.Output;

namespace Desafio.Core.Contratos
{
    public interface ITheMovieDb
    {
        Estreias ObterTodasEstreias();

        Estreias ObterEstreias(int pagina = 1);
    }
}

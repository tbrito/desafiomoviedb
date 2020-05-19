using Desafio.Core.Output;

namespace Desafio.Core.Contratos
{
    public interface IMovieApi
    {
        Estreias ObterTodasEstreias();

        Estreias ObterEstreias(int pagina = 1);
    }
}

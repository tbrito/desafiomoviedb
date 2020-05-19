using Newtonsoft.Json;

namespace Desafio.Core.Output
{
    public class Genero
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Nome { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Desafio.Core.Output
{
    [JsonObject("genres")]
    public class Generos
    {
        [JsonProperty("genres")]
        public List<Genero> Genero { get; set; }
    }
}

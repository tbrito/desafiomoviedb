using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Desafio.Core.Output
{
    public class Estreias
    {
        [JsonProperty("results")]
        public List<Filme> Filmes { get; set; }

        [JsonProperty("page")] 
        public int Pagina { get; set; }

        [JsonProperty("total_results")] 
        public int TotalResultados { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPaginas { get; set; }
    }
}

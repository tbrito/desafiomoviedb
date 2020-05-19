using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Desafio.Core.Output
{
    [JsonObject("results")]
    public class Filme
    {
        public Filme()
        {
            this.Generos = new List<Genero>();
        }

        public int Id { get; set; }

        [JsonProperty("popularity")]
        public double Popularidade { get; set; }

        [JsonProperty("vote_count")]
        public int Votos { get; set; }

        public bool Video { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("title")]
        public string Titulo { get; set; }

        public bool Adulto { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("original_language")]
        public string LinguaOriginal { get; set; }

        [JsonProperty("overview")]
        public string Resumo { get; set; }

        [JsonProperty("genre_ids")] 
        public IEnumerable<int> GeneroIds { get; set; }

        [JsonProperty("release_date")] 
        public string LancamentoEm { get; set; }

        public List<Genero> Generos { get; set; }
    }
}
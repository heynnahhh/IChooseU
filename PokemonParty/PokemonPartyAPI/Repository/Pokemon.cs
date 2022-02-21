using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace PokeAPI.Net.Repository
{
    public class Types
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pokemon")]
        public List<TypePokemon> Pokemon { get; set; }
    }

    public class TypePokemon
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("pokemon")]
        public JObject Pokemon { get; set; }
    }

}

using Newtonsoft.Json;
using pokemons_back.Core.Models;

namespace pokemons_back.Core.Contracts;

public class StatInfoDto
{
    [JsonProperty(PropertyName = "Base_Stat")]
    public int Base_Stat { get; set; }
    public Stat Stat { get; set; } = null!;
}
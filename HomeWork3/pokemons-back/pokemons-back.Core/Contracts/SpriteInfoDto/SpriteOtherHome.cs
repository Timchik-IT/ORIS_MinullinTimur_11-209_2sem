using Newtonsoft.Json;

namespace pokemons_back.Core.Contracts.SpriteInfoDto;

public class SpriteOtherHome
{
    [JsonProperty(PropertyName = "Front_Default")]
    public string Front_Default { get; set; } = "";
}
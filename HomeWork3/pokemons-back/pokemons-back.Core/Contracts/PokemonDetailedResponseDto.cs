namespace pokemons_back.Core.Contracts;

public class PokemonDetailedResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public int Height { get; set; }
    
    public int Weight { get; set; }

    public List<MoveInfoDto> Moves { get; set; } = null!;

    public List<AbilityInfoDto> Abilities { get; set; } = null!;

    public SpriteInfoDto.SpriteInfoDto Sprites { get; set; } = null!;

    public List<TypeInfoDto> Types { get; set; } = null!;

    public List<StatInfoDto> Stats { get; set; } = null!;
}
namespace pokemons_back.Core.Contracts;

public class PokemonResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public SpriteInfoDto.SpriteInfoDto Sprites { get; set; } = null!;

    public List<TypeInfoDto> Types { get; set; } = null!; 
}
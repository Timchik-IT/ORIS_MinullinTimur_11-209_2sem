using pokemons_back.Core.Models;

namespace pokemons_back.Core.Contracts;

public class PokemonRequestDto
{
    public List<Pokemon> Results { get; init; } = new();
}
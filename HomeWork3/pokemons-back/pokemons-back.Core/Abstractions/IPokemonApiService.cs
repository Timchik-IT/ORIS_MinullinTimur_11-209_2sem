using pokemons_back.Core.Contracts;

namespace pokemons_back.Core.Abstractions;

public interface IPokemonApiService
{
    Task<List<PokemonResponseDto>> GetByFilterAsync(string filter = "", int limit = 20, int offset = 0);
    Task<PokemonDetailedResponseDto?> GetByIdOrNameAsync(string idOrName);
}
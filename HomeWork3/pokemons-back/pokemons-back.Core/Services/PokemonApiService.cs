using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using pokemons_back.Core.Abstractions;
using pokemons_back.Core.Contracts;

namespace pokemons_back.Core.Services;

public class PokemonApiService : IPokemonApiService
{
    private readonly HttpClient _httpClient = new();
    private const string? PokemonApiUrl = "https://pokeapi.co/api/v2/pokemon";

    public async Task<List<PokemonResponseDto>> GetByFilterAsync(string filter = "", int limit = 20, int offset = 0)
    {
        var response = await _httpClient.GetStringAsync(PokemonApiUrl + $"?limit={limit}" + $"&offset={offset}");
        var pokemonList = JsonConvert.DeserializeObject<PokemonRequestDto>(response);

        if (pokemonList is null)
            throw new NullReferenceException("Pokemons are not existing anymore!");
        
        var pokemonListFiltered = pokemonList.Results
            .Where(i => i.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
        var pokemonDetailedList = new List<PokemonDetailedResponseDto>();
        foreach (var pokemon in pokemonListFiltered)
        {
            var pokemonDetailed = await _httpClient.GetFromJsonAsync<PokemonDetailedResponseDto>(pokemon.Url);

            if (pokemonDetailed is null)
                throw new NullReferenceException("Pokemon does not exist!");
            pokemonDetailedList.Add(pokemonDetailed);
        }

        var pokemonResponseDtoList = pokemonDetailedList
            .Select(MapPokemonDetailedToPokemonResponseDto)
            .ToList();

        return pokemonResponseDtoList;
    }

    public async Task<PokemonDetailedResponseDto?> GetByIdOrNameAsync(string idOrName)
    {
        var response = await _httpClient.GetAsync(PokemonApiUrl + $"/{idOrName.ToLower()}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        var responseData = await response.Content.ReadAsStringAsync();
        var pokemonDetailed = JsonConvert.DeserializeObject<PokemonDetailedResponseDto>(responseData);

        return pokemonDetailed;

    }

    private static PokemonResponseDto MapPokemonDetailedToPokemonResponseDto(PokemonDetailedResponseDto pokemonDetailedResponse)
    {
        return new PokemonResponseDto
        {
            Id = pokemonDetailedResponse.Id,
            Name = pokemonDetailedResponse.Name,
            Sprites = pokemonDetailedResponse.Sprites,
            Types = pokemonDetailedResponse.Types
        };
    }
}
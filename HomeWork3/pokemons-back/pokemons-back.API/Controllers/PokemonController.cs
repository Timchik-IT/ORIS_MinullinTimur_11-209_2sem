using Microsoft.AspNetCore.Mvc;
using pokemons_back.Core.Abstractions;

namespace pokemons_back.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonApiService _pokemonApiService;

    public PokemonController(IPokemonApiService pokemonApiService)
    {
        _pokemonApiService = pokemonApiService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(int limit, int offset)
    {
        var pokemonDataDtoList = await _pokemonApiService.GetByFilterAsync("", limit, offset);
        return Ok(pokemonDataDtoList);
    }
    
    [HttpGet]
    [Route("{filter}")]
    public async Task<IActionResult> GetByFilter(int limit, int offset, string filter)
    {
        var pokemonsListDataDto = await _pokemonApiService.GetByFilterAsync(filter);
        return Ok(pokemonsListDataDto);
    }
    
    [HttpGet]
    [Route("{idOrName}")]
    public async Task<IActionResult> GetByIdOrName(string idOrName)
    {
        var pokemonDataDto = await _pokemonApiService.GetByIdOrNameAsync(idOrName);

        if (pokemonDataDto is null)
            return NotFound();
 
        return Ok(pokemonDataDto);
    }
}
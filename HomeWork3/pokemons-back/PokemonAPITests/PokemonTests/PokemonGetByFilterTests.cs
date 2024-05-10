using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonsAPI.Models.DTOs;

namespace PokemonAPITests.PokemonTests;

[TestClass]
public class PokemonGetByFilterTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5178/Pokemon/GetByFilter";

    [TestMethod]
    [DataRow("bulba")]
    [DataRow("arch")]
    public async Task AllPokemonNamesContainFilterString(string filter)
    {
        var specificUrl = Url + $"/{filter}";

        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null)
            throw new NullReferenceException("Source was empty");

        var ifAllPokemonNamesContainFilterString = responseJson
            .All(i => CheckIfPokemonNameContainFilterString(i, filter));
        Assert.IsTrue(ifAllPokemonNamesContainFilterString);
    }

    [TestMethod]
    [DataRow("Timerhkan")]
    [DataRow("BadFilter")]
    public async Task WrongFilterReturnsEmptyList(string filter)
    {
        var specificUrl = Url + $"/{filter}";

        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null)
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(0, responseJson.Count);
    }

    [TestMethod]
    [DataRow("bulba", "BuLbA")]
    [DataRow("arch", "ARCH")]
    public async Task FilterIsNotCaseSensitive(string lowercase, string someCase)
    {
        var specificUrl = Url + $"/{lowercase}";

        var firstResponse = await _httpClient.GetStringAsync(specificUrl);
        var firstResponseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(firstResponse);

        var secondResponse = await _httpClient.GetStringAsync(specificUrl);
        var secondResponseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(secondResponse);

        if (firstResponseJson is null || secondResponseJson is null)
            throw new NullReferenceException("Source was empty");

        Assert.AreEqual(firstResponseJson.Count, secondResponseJson.Count);
    }

    private static bool CheckIfPokemonNameContainFilterString(PokemonResponseDto pokemonResponseDto, string filter) =>
        pokemonResponseDto.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
}
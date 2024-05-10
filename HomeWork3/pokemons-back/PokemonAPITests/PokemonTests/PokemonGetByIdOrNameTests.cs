using System.Net;
using Newtonsoft.Json;
using PokemonsAPI.Models.DTOs;

namespace PokemonAPITests.PokemonTests;

[TestClass]
public class PokemonGetByIdOrNameTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5178/Pokemon/GetByIdOrName";

    [TestMethod]
    [DataRow(1)]
    [DataRow(500)]
    public async Task GetByIdWorksRight(int id)
    {
        var specificUrl = Url + $"/{id}";
        
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonDetailedResponseDto>(response);
        
        Assert.IsTrue(responseJson is not null && responseJson.Name.Any());
    }
    
    [TestMethod]
    [DataRow("wartortle")]
    [DataRow("KAKUNA")]
    public async Task GetByNameWorksRight(string name)
    {
        var specificUrl = Url + $"/{name}";
        
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<PokemonDetailedResponseDto>(response);
        
        Assert.IsTrue(responseJson is not null && responseJson.Name.Any());
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("BadName")]
    public async Task WrongNameOrIdReturnsNotFound(string idOrName)
    {
        var specificUrl = Url + $"/{idOrName}";
        
        var response = await _httpClient.GetAsync(specificUrl);
        
        Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
    }
}
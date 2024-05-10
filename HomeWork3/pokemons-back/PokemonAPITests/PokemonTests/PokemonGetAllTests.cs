using Newtonsoft.Json;
using PokemonsAPI.Models.DTOs;

namespace PokemonAPITests.PokemonTests;

[TestClass]
public class PokemonGetAllTests
{
    private readonly HttpClient _httpClient = new();
    private const string Url = "http://localhost:5178/Pokemon/GetAll";
        
    [TestMethod]
    [DataRow(10)]
    public async Task LimitWorksRight(int limit)
    {
        // Arrange
        var specificUrl = Url + $"?limit={limit}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null || responseJson.Count == 0)
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(limit, responseJson.Count);
    }
    
    [TestMethod]
    public async Task Limit0Returns20()
    {
        // Arrange
        const string specificUrl = Url + "?limit=0";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null || responseJson.Count == 0)
            throw new NullReferenceException("Source was empty");

        Assert.AreEqual(20, responseJson.Count);
    }
    
    [TestMethod]
    public async Task NoLimitReturns20()
    {
        var response = await _httpClient.GetStringAsync(Url);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null || responseJson.Count == 0)
            throw new NullReferenceException("Source was empty");

        Assert.AreEqual(20, responseJson.Count);
    }
    
    [TestMethod]
    [DataRow(1)]
    [DataRow(50)]
    public async Task OffsetWorksRight(int offset)
    {
        // Arrange
        var specificUrl = Url + $"?offset={offset}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null || responseJson.Count == 0)
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.AreEqual(offset + 1, responseJson.First().Id);
    }

    [TestMethod]
    [DataRow(10, 15)]
    public async Task LimitAndOffsetWorkRight(int limit, int offset)
    {
        // Arrange
        var specificUrl = Url + $"?limit={limit}" + $"&offset={offset}";
        
        // Act
        var response = await _httpClient.GetStringAsync(specificUrl);
        var responseJson = JsonConvert.DeserializeObject<List<PokemonResponseDto>>(response);

        if (responseJson is null || responseJson.Count == 0)
            throw new NullReferenceException("Source was empty");

        // Assert
        Assert.IsTrue(offset + 1 == responseJson.First().Id && responseJson.Count == limit);
    }
}
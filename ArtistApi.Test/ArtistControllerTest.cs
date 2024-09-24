using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Checkpoint.Test;

public class ArtistControllerTest
{
    private WebApplicationFactory<Program> _factory;
    
    public ArtistControllerTest()
    {
        _factory = new WebApplicationFactory<Program>();
    }


    [Fact]
    public async Task TestGetArtist_ReturnsAllArtists()
    {
        // Arrange
        var client = _factory.CreateClient();
     
        // Act
        var response = await client.GetAsync("/artists");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync < List<Artist.Artist >> ();
    
        // Assert
        Assert.NotNull(content);
        Assert.True(content.Count > 0);
        Console.WriteLine("number of albums: "+ content.Count);
    }
    
    // deprecated after change in get by id
    // [Theory]
    // [InlineData("12Chz98pHFMPJEknJQMWvI")] 
    // public async Task TestGetArtistById_ReturnsArtist(string id)
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();
    //  
    //     // Act
    //     var response = await client.GetAsync($"/artists/{id}");
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadFromJsonAsync < Artist.Artist > ();
    //
    //     // Assert
    //     Assert.NotNull(content);
    //     Assert.True(content.ArtistName == "Muse");
    // }

    [Theory]
    [InlineData("12Chz9ss8pHTsEaaSTEknJQMWvI", "Jinjer")]
    public async Task TestAddArtist_ReturnsArtist(string id, string name)
    {
        // Arrange
        var client = _factory.CreateClient();
        // var artist = new Artist.Artist(id, name);
    
        // Act
        var response = await client.PostAsync($"/artists?id={id}&name={name}", null);
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData("3fMbdgg4jU18AjLCKBhRSm")]  
    public async Task DeleteArtist_RemovesArtistFromList(string id)
    {
        // Arrange
        var client = _factory.CreateClient();
            
        // Act
        var firstRes = await client.GetAsync("/artists");
        var content = await firstRes.Content.ReadFromJsonAsync< List<Artist.Artist >>();
        var initialCount = content.Count;
    
        var res = await client.DeleteAsync($"/artists/{id}");
    
        var res2 = await client.GetAsync("/artists");
        var content2 = await res2.Content.ReadFromJsonAsync < List<Artist.Artist >> ();
    
        // Assert
        var expected = initialCount - 1;
        Assert.Equal(content2.Count, expected);
    }
}
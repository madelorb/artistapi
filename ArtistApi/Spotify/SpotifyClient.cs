namespace Checkpoint.Spotify;

public class SpotifyClient : ISpotifyClient 
{
    private readonly HttpClient _client;
    
    public SpotifyClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<SpotifyArtistDetails?> GetArtist(string id)
    {
        var requestUri = $"{id}";
        var response = await _client.GetAsync(requestUri);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<SpotifyArtistDetails>();
        return null;
    }
}
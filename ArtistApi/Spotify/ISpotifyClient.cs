namespace Checkpoint.Spotify;

public interface ISpotifyClient
{
    Task<SpotifyArtistDetails?> GetArtist(string id);
}

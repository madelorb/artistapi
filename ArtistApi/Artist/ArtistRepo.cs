namespace Checkpoint.Artist;

public class ArtistRepo : IArtistsRepo
{
    private static List<Artist> _artists = new List<Artist>() { 
        { new Artist("711MCceyCBcFnzjGY4Q7Un", "AC/DC") },
        { new Artist("3fMbdgg4jU18AjLCKBhRSm", "Micael Jackson") },
        { new Artist("1OTNNdgU6qLUTCwvJxcObX", "Knutsen & Ludvigsen") },
        { new Artist("12Chz98pHFMPJEknJQMWvI", "Muse") },
        { new Artist("4TrraAsitQKl821DQY42cZ", "Sigrid") },
    };
    public static List<Artist> GetAllArtists()
    {            return _artists;         }
    
    public static void AddArtist(Artist artist) => _artists.Add(artist);
    public static void DeleteArtist(Artist artist) => _artists.Remove(artist);

}
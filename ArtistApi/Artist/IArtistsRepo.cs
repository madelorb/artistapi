namespace Checkpoint.Artist;

public interface IArtistsRepo
{
    public interface IArtistRepo
    {
        public List<Artist> GetAllArtists();    
        public void AddArtist(Artist artist);
    }
    

}
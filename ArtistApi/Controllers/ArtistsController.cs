using Microsoft.AspNetCore.Mvc;
using Checkpoint.Artist;
using Checkpoint.Spotify;
using static Checkpoint.Artist.ArtistRepo;

namespace Checkpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly ILogger<ArtistsController> _logger;
    
    public ArtistsController(ISpotifyClient spotifyClient, ILogger<ArtistsController> logger)
    {
        _spotifyClient = spotifyClient;
        _logger = logger;
    }
    
    [HttpGet()]
    public IActionResult GetAll()
    {
        return Ok(GetAllArtists());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SpotifyArtistDetails>> GetArtist(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        var artist = await _spotifyClient.GetArtist(id);
        if(artist == null)
        {
            return NotFound(new { Message = "Artist not found" });
        }
        
    
        return Ok(artist);
    }
    
    [HttpPost("")]
    public IActionResult CreateArtist([FromQuery]string id, string name)
    {
        var artist = new Artist.Artist(id, name);
        try
        {
            ArtistRepo.AddArtist(artist);
            return Ok(artist);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
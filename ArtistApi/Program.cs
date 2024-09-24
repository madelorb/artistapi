using Checkpoint.Artist;
using Checkpoint.Spotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Spotify API configuration
string? spotifyApiHost = builder.Configuration.GetSection("Spotify")["ApiHost"];
string? spotifyApiToken = Environment.GetEnvironmentVariable("spotify-secret");
;
// Log the retrieved token for debugging purposes
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Retrieved Spotify API token: {spotifyApiToken ?? "Token not found"}");
builder.Services.AddHttpClient<ISpotifyClient, SpotifyClient>(client =>
{
    if (spotifyApiHost != null) client.BaseAddress = new Uri($"{spotifyApiHost}/v1/artists/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Checkpoint/1.0");
    
    // if (spotifyApiToken != null) client.DefaultRequestHeaders.Authorization =  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", spotifyApiToken);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {spotifyApiToken}");
});
string? hisSecret = Environment.GetEnvironmentVariable("his-secret");
logger.LogInformation($"Retrieved Spotify API token: {hisSecret ?? "Token not found"}");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
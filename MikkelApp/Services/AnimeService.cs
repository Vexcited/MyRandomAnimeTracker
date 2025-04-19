using System.Text.Json;
using MikkelApp.Models;

namespace MikkelApp.Services;

public class AnimeService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.jikan.moe/v4";
    private static List<Anime> _customAnimes = new List<Anime>();

    public AnimeService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<List<Anime>> GetTopAnimeAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/top/anime");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var root = jsonDocument.RootElement;
            var data = root.GetProperty("data");

            var animes = new List<Anime>();
            foreach (var item in data.EnumerateArray())
            {
                var anime = new Anime
                {
                    MalId = item.GetProperty("mal_id").GetInt32(),
                    Title = item.GetProperty("title").GetString() ?? string.Empty,
                    TitleEnglish = item.GetProperty("title_english").GetString(),
                    TitleJapanese = item.GetProperty("title_japanese").GetString(),
                    Synopsis = item.GetProperty("synopsis").GetString() ?? string.Empty,
                    Score = item.GetProperty("score").GetDouble(),
                    ImageUrl = item.GetProperty("images").GetProperty("jpg").GetProperty("image_url").GetString() ?? string.Empty,
                    Type = item.GetProperty("type").GetString() ?? string.Empty,
                    Episodes = item.GetProperty("episodes").GetInt32(),
                    Status = item.GetProperty("status").GetString() ?? string.Empty,
                    Rating = item.GetProperty("rating").GetString() ?? string.Empty
                };
                animes.Add(anime);
            }

            // Add custom animes to the list
            animes.AddRange(_customAnimes);
            return animes;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching anime data: {ex.Message}");
            return _customAnimes; // Return at least the custom animes if API fails
        }
    }

    public Task AddAnimeAsync(Anime anime)
    {
        // Generate a unique negative ID for custom animes
        anime.MalId = -(_customAnimes.Count + 1);
        _customAnimes.Add(anime);
        return Task.CompletedTask;
    }
}
